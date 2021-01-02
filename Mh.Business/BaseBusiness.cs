using Dapper;
using Mh.Business.Bo.Sys;
using Mh.Business.Log;
using Mh.Utils;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business
{
    public class BaseBusiness
    {
        protected string GetDicValue(int Id, Enums.Languages language, params string[] param)
        {
            return GetDicValue(Id, language, param);
        }
        protected string GetDicValue(string key, Enums.Languages language, params string[] param)
        {
            return Stc.GetDicValue(key, language, param);
        }
        //protected string GetDicValue(string key, params string[] param)
        //{
        //    Enums.Languages language = Enums.Languages.Turkce;
        //    if (Session != null)
        //    {
        //        RealPersonBo realPersonBo = Session.RealPerson;
        //        if (realPersonBo != null)
        //            language = realPersonBo.LanguageId;
        //    }

        //    return GetDicValue(key, language, param);
        //}
        protected string GetbyLang(DicItem dicItem, Enums.Languages language)
        {
            return Stc.GetbyLang(dicItem, language);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="baseBo"></param>
        /// <param name="applicationTypeId">Fill this param only if it is 'Angular' otherwise keep it null.</param>
        public ResponseBo SaveExLog(Exception exception, Type type, string methodName, BaseBo baseBo, Enums.ApplicationTypes? applicationTypeId = null)
        {
            LogExceptionBusiness exceptionBusiness = new LogExceptionBusiness();

            if (applicationTypeId == null)
            {
                applicationTypeId = Enums.ApplicationTypes.Unknown;

                string fullName = type.FullName.Replace(".", "");

                if (fullName.StartsWith(Enums.ApplicationTypes.MhBusinessBo.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhBusinessBo;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhBusinessContract.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhBusinessContract;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhBusiness.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhBusiness;

                else if (fullName.StartsWith(Enums.ApplicationTypes.MhEnums.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhEnums;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhSessions.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhSessions;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhUtils.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhUtils;

                else if (fullName.StartsWith(Enums.ApplicationTypes.MhDb.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhDb;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhDbAccess.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhDbAccess;

                else if (fullName.StartsWith(Enums.ApplicationTypes.websocketsharp.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.websocketsharp;

                else if (fullName.StartsWith(Enums.ApplicationTypes.MhServiceCurrencyCollect.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhServiceCurrencyCollect;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhServiceDto.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhServiceDto;
                else if (fullName.StartsWith(Enums.ApplicationTypes.MhServiceWebApi.ToString()))
                    applicationTypeId = Enums.ApplicationTypes.MhServiceWebApi;
            }

            string clientIpAddress = null;
            long? apiSessionId = null;
            if (baseBo != null)
            {
                clientIpAddress = baseBo.Session.ClientIpAddress;
                apiSessionId = baseBo.Session.ApiSessionId;
            }

            ResponseBo responseBo = exceptionBusiness.Save(exception, applicationTypeId.Value, type.Name, methodName, clientIpAddress, apiSessionId);
            responseBo.IsSuccess = false;

            Enums.Languages languageId = Enums.Languages.xTurkish;
            if (baseBo != null && baseBo.Session != null && baseBo.Session.RealPerson != null)
            {
                languageId = baseBo.Session.RealPerson.LanguageId;
            }

            responseBo.Message = GetDicValue("xUnexpectedErrorOccurred", languageId);

            if (responseBo.ReturnedId != null)
            {
                responseBo.Message += Environment.NewLine + GetDicValue("xTextErrorNumber", languageId) + ":" + Environment.NewLine + responseBo.ReturnedId.Value.ToString();
            }

            return responseBo;
        }

        protected string GetLocationFromIp(string ip)
        {
            if (Validation.ValidateIPv4(ip) == false) return null;

            string result = null;

            try
            {
                result = IPInfo.Get(ip, "9ce6a1398f1200");
            }
            catch
            {
                result = null;
            }

            return result;
        }

        protected ResponseBo GetNextId(string seName)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.ReturnedId = conn.Query<long>($"select next value for {seName}", commandType: CommandType.Text).FirstOrDefault();
                    //responseBo.Bo = conn.Query<FicheListBo>("spFicheList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = null;
                    responseBo.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                responseBo = SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        protected void AddStandartSpParams(ref DynamicParameters p, BaseBo baseBo)
        {
            // Outputs
            p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
            p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);

            // Inputs
            p.Add("@ApiSessionId", baseBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
            p.Add("@MyPersonId", baseBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
            p.Add("@OperatorRealId", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
            p.Add("@LanguageId", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
        }
    }
}