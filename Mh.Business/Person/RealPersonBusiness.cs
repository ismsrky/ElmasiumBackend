using Dapper;
using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Utils;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class RealPersonBusiness : BaseBusiness, IRealPersonBusiness
    {
        public ResponseBo<RealPersonBo> Get(BaseBo baseBo)
        {
            ResponseBo<RealPersonBo> responseBo = new ResponseBo<RealPersonBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@OperatorRealId", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<RealPersonBo>("spRealGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<RealPersonBo>();
            }

            return responseBo;
        }
        public ResponseBo Update(RealPersonBo realPersonBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                #region Checks
                if (realPersonBo == null)
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xInvalidData", realPersonBo.Session.RealPerson.LanguageId);

                    return responseBo;
                }
                if (realPersonBo.Name.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xFirstNameCantBeEmpty", realPersonBo.Session.RealPerson.LanguageId);

                    return responseBo;
                }
                if (realPersonBo.Surname.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xLastNameCantBeEmpty", realPersonBo.Session.RealPerson.LanguageId);

                    return responseBo;
                }

                #endregion

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId", realPersonBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Name", realPersonBo.Name, DbType.String, ParameterDirection.Input);
                    p.Add("@Surname", realPersonBo.Surname, DbType.String, ParameterDirection.Input);
                    p.Add("@Birthdate", realPersonBo.Birthdate, DbType.Date, ParameterDirection.Input);
                    p.Add("@GenderId", realPersonBo.GenderId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@DefaultCurrencyId", realPersonBo.DefaultCurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Phone", realPersonBo.Phone, DbType.String, ParameterDirection.Input);

                    p.Add("@LanguageId", realPersonBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spRealUpdate", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, realPersonBo);
            }

            return responseBo;
        }
      

        public ResponseBo ChangeLanguage(BaseBo baseBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId ", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("Update Person set LanguageId=@LanguageId,UpdateDate=getdate(),UpdatePersonId=@Id where Id=@Id", p, commandType: CommandType.Text);
                }

                responseBo.IsSuccess = true;
                responseBo.Message = null;
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo);
            }

            return responseBo;
        }
    }
}