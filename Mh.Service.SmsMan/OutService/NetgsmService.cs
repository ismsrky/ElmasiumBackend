using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.SmsMan.OutService
{
    public class NetgsmService : ISmsService
    {
        public string mer { get; set; }
        public string UrlAddress { get; set; }
        public string OtpUrlAddress { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Caption { get; set; }
        public string TextMessage { get; set; }

        public string CompanyName { get; set; }
        public bool IsSupportTr { get; set; }

        public List<string> PhoneNumberList { get; set; }

        public ServiceReturnBo Send()
        {
            ServiceReturnBo returnBo = new ServiceReturnBo();
            string ss = "";
            ss += "<?xml version='1.0' encoding='UTF-8'?>";
            ss += "<mainbody>";
            ss += "<header>";
            ss += $"<company{(IsSupportTr ? " dil='TR'" : "")}>{CompanyName}</company>";
            ss += $"<usercode>{Username}</usercode>";
            ss += $"<password>{Password}</password>";
            ss += "<type>1:n</type>";
            ss += $"<msgheader>{Caption}</msgheader>";
            ss += "</header>";
            ss += "<body>";
            ss += "<msg>";
            ss += $"<![CDATA[{TextMessage}]]>";
            ss += "</msg>";

            foreach (string item in PhoneNumberList)
            {
                ss += $"<no>{item}</no>";
            }

            ss += "</body>  ";
            ss += "</mainbody>";

            returnBo.ReturnValue = Utils.Net.postXml(UrlAddress, ss);

            if (returnBo.ReturnValue.StartsWith("00"))
            {
                returnBo.IsSuccess = true;
            }
            else
            {
                returnBo.IsSuccess = false;
            }

            return returnBo;
        }
        public ServiceReturnBo SendOtp()
        {
            return null;
        }


        private string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
    }
}