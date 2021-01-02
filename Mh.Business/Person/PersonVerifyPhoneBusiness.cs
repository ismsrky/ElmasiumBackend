using Dapper;
using Mh.Business.Bo.Person.VerifyPhone;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class PersonVerifyPhoneBusiness : BaseBusiness, IPersonVerifyPhoneBusiness
    {
        public ResponseBo<PersonVerifyPhoneGenReturnBo> Gen(PersonVerifyPhoneGenBo genBo)
        {
            ResponseBo<PersonVerifyPhoneGenReturnBo> responseBo = new ResponseBo<PersonVerifyPhoneGenReturnBo>();
           
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, genBo);

                    p.Add("@PersonId", genBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Phone", genBo.Phone, DbType.String, ParameterDirection.Input, 50);

                    responseBo.Bo = conn.Query<PersonVerifyPhoneGenReturnBo>("spPersonVerifyPhoneGen", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, genBo).ToResponse<PersonVerifyPhoneGenReturnBo>();
            }

            return responseBo;
        }

        public ResponseBo Save(PersonVerifyPhoneSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@VerifyCode", saveBo.VerifyCode, DbType.String, ParameterDirection.Input, 50);

                    conn.Execute("spPersonVerifyPhoneSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }

        public ResponseBo IsVerified(PersonVerifyPhoneGenBo genBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, genBo);

                    p.Add("@PersonId", genBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Phone", genBo.Phone, DbType.String, ParameterDirection.Input, 50);

                    conn.Execute("spPersonVerifyPhoneIs", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, genBo);
            }

            return responseBo;
        }
    }
}