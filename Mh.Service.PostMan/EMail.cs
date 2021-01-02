using IronPdf;
using Mh.Business.Bo.Notification.EMail;
using Mh.Business.Bo.Sys;
using Mh.Business.Notification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mh.Service.PostMan
{
    public class EMail
    {
        public void Send(NotificationEMailListBo eMailBo)
        {
            if (Stc.StartedList.Count(x => x == eMailBo.Id) > 0) return;

            //if (eMailBo.ReceiverList.Count(x => x.Receiver != "ismail.sarikaya@elmasium.com") > 0) return;
            Stc.StartedList.Add(eMailBo.Id);
            SaveLog(eMailBo, Enums.EMailLogEvents.xStarted, null);
            Stc.sayi = 1;

            try
            {
                Business.Sys.SysBusiness sysBusiness = new Business.Sys.SysBusiness();
                SysMailBo sysMailBo = Stc.SysMailList.FirstOrDefault(x => x.Id == sysBusiness.GetSysMailId(eMailBo.SubjectTypeId));

                //string displayName = "Elmasium - " + Business.Stc.GetDicValue(sysMailBo.DisplayName, eMailBo.LanguageId);
                //if (eMailBo.SubjectTypeId == Enums.EMailSubjectTypes.xWelcome)
                //    displayName = Business.Stc.GetDicValue("xWelcomeElmasium", eMailBo.LanguageId);

                eMailBo.Content = Business.Stc.DictionaryProcessText(eMailBo.Content, eMailBo.LanguageId);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(eMailBo.Sender, eMailBo.SenderDisplayName);
                mail.Body = eMailBo.Content;
                mail.IsBodyHtml = eMailBo.IsContentHtml;
                mail.Subject = eMailBo.Subject;

                foreach (NotificationEMailReceiverListBo item in eMailBo.ReceiverList)
                {
                    switch (item.ReceiverTypeId)
                    {
                        case Enums.EMailReceiverTypes.To:
                            mail.To.Add(new MailAddress(item.Receiver));
                            break;
                        case Enums.EMailReceiverTypes.Cc:
                            mail.CC.Add(new MailAddress(item.Receiver));
                            break;
                        case Enums.EMailReceiverTypes.Bcc:
                            mail.Bcc.Add(new MailAddress(item.Receiver));
                            break;
                        default:
                            mail.To.Add(new MailAddress(item.Receiver));
                            break;
                    }
                }

                if (eMailBo.AttachList != null && eMailBo.AttachList.Count() > 0)
                {
                    string outputPath = null;

                    foreach (NotificationEMailAttachListBo item in eMailBo.AttachList)
                    {
                        outputPath = Path.Combine(Stc.PdfFileDirectory, item.UniqueId.ToString().ToUpper() + ".pdf");
                        item.HtmlRaw = Business.Stc.DictionaryProcessText(item.HtmlRaw, eMailBo.LanguageId);

                        SaveAttach(item, outputPath);

                        Attachment attachment = new Attachment(outputPath);
                        attachment.Name = item.PseudoFileName;

                        mail.Attachments.Add(attachment);
                    }

                    SaveLog(eMailBo, Enums.EMailLogEvents.xAttachFilesSavedSuccess, null);
                }

                SmtpClient smtp = new SmtpClient
                {
                    Host = sysMailBo.Host,
                    Port = sysMailBo.Port,
                    EnableSsl = sysMailBo.Ssl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail.From.Address, sysMailBo.Password)
                };

                smtp.SendCompleted += (s, e) =>
                {
                    mail.Dispose();
                    smtp.Dispose();

                    ResponseBo exResponseBo = null;
                    if (e.Error != null)
                    {
                        exResponseBo = SaveExLog(e.Error, this.GetType(), MethodBase.GetCurrentMethod().Name);
                    }

                    SaveLog(eMailBo, Enums.EMailLogEvents.xTransactionSuccessful, exResponseBo?.ReturnedId);

                    SaveSent(eMailBo, true);
                };

                smtp.SendAsync(mail, null);
            }
            catch (Exception ex)
            {
                ResponseBo exResponseBo = SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name);

                SaveLog(eMailBo, Enums.EMailLogEvents.xUnexpectedErrorOccurred, exResponseBo.ReturnedId);
                SaveSent(eMailBo, false);
            }
        }

        void SaveAttach(NotificationEMailAttachListBo attach, string outputPath)
        {
            PdfPrintOptions options = new PdfPrintOptions();
            options.MarginBottom = 0;
            options.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            options.MarginLeft = 5;
            options.MarginRight = 5;
            options.MarginTop = 5;
            options.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Landscape;

            string str = null;
            //PdfDocument pdf = HtmlToPdf.StaticRenderHtmlAsPdf(attach.HtmlRaw, str, options);

            HtmlToPdf pdf = new HtmlToPdf(options);
            PdfDocument pdfDocument = pdf.RenderHtmlAsPdf(attach.HtmlRaw);
            pdfDocument.TrySaveAs(outputPath);
        }

        void SaveLog(NotificationEMailListBo eMailBo, Enums.EMailLogEvents emailEventId, long? logExceptionId)
        {            
            NotificationEMailBusiness eMailBusiness = new NotificationEMailBusiness();
            eMailBusiness.SaveLog(new NotificationEMailLogBo()
            {
                NotificationEMailId = eMailBo.Id,

                EmailEventId = emailEventId,
                LogExceptionId = logExceptionId
            });
        }
        void SaveSent(NotificationEMailListBo eMailBo,bool sentSuccessfully)
        {
            NotificationEMailBusiness eMailBusiness = new NotificationEMailBusiness();
            eMailBusiness.SaveSent(new NotificationEMailSentSaveBo()
            {
                NotificationEMailId = eMailBo.Id,

                Content = eMailBo.Content,

                SentSuccessfully = sentSuccessfully
            });
        }

        ResponseBo SaveExLog(Exception exception, Type type, string methodName)
        {
            Business.BaseBusiness baseBusiness = new Business.BaseBusiness();

            return baseBusiness.SaveExLog(exception, type, methodName, null, Enums.ApplicationTypes.MhServicePostMan);
        }
    }
}