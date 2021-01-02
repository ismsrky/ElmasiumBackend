using Dapper;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Sys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Sys
{
    public class SysBusiness : BaseBusiness, ISysBusiness
    {
        public ResponseBo<List<SysMailBo>> GetMailList()
        {
            ResponseBo<List<SysMailBo>> responseBo = new ResponseBo<List<SysMailBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<SysMailBo>(
                        @"select
                        M.Id, M.Email, M.[Password], M.Host, M.[Port], M.[Ssl], M.DisplayName
                        from SysMail M", commandType: CommandType.Text).ToList();
                }

                foreach (SysMailBo item in responseBo.Bo)
                {
                    item.Password = Utils.Encryption.DecryptString(item.Password);
                }

                responseBo.Message = null;
                responseBo.IsSuccess = true;
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<SysMailBo>>();
            }

            return responseBo;
        }

        public int GetSysMailId(Enums.EMailSubjectTypes subjectTypeId)
        {
            // Following values come from table 'EnumEMailSubjectType'.
            switch (subjectTypeId)
            {
                case Enums.EMailSubjectTypes.xDailySummary:
                    return 0;
                case Enums.EMailSubjectTypes.xWeeklySummary:
                    return 0;
                case Enums.EMailSubjectTypes.xMonthlySummary:
                    return 0;
                case Enums.EMailSubjectTypes.xAnnualSummary:
                    return 0;

                case Enums.EMailSubjectTypes.xForgotPassword:
                    return 1;
                case Enums.EMailSubjectTypes.xWelcome:
                    return 1;
                case Enums.EMailSubjectTypes.xEmailVerification:
                    return 1;

                case Enums.EMailSubjectTypes.xNewShop:
                    return 2;
                case Enums.EMailSubjectTypes.xPendingRequest:
                    return 2;
                case Enums.EMailSubjectTypes.xConnectionAccepted:
                    return 2;
                case Enums.EMailSubjectTypes.xConnectionRejected:
                    return 2;
                case Enums.EMailSubjectTypes.xConnectionDeleted:
                    return 2;

                case Enums.EMailSubjectTypes.xFicheRequest:
                    return 2;
                case Enums.EMailSubjectTypes.xFicheAccepted:
                    return 2;
                case Enums.EMailSubjectTypes.xFicheRejected:
                    return 2;

                case Enums.EMailSubjectTypes.xInfo:
                    return 2;
                default:
                    return 2;
            }
        }

        public ResponseBo<List<SysSmsBo>> GetSmsList()
        {
            ResponseBo<List<SysSmsBo>> responseBo = new ResponseBo<List<SysSmsBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<SysSmsBo>(
                        @"select S.Id, S.UrlAddress, S.OtpUrlAddress, S.Username, S.[Password], S.CompanyName from SysSms S", commandType: CommandType.Text).ToList();
                }

                foreach (SysSmsBo item in responseBo.Bo)
                {
                    item.Password = Utils.Encryption.DecryptString(item.Password);
                }

                responseBo.Message = null;
                responseBo.IsSuccess = true;
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<SysSmsBo>>();
            }

            return responseBo;
        }

        public ResponseBo<SysVersionBo> GetLatestVersion(SysVersionGetLatestCriteriaBo criteriaBo)
        {
            ResponseBo<SysVersionBo> responseBo = new ResponseBo<SysVersionBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@ApplicationTypeId", criteriaBo.ApplicationTypeId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<SysVersionBo>("spSysVersionGetLatest", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<SysVersionBo>();
            }

            return responseBo;
        }
    }
}