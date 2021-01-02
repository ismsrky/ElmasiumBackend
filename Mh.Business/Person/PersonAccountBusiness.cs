using Dapper;
using Mh.Business.Bo.Person.Account;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class PersonAccountBusiness : BaseBusiness, IPersonAccountBusiness
    {
        public ResponseBo<PersonAccountBo> Get(PersonAccountGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonAccountBo> responseBo = new ResponseBo<PersonAccountBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@AccountId", criteriaBo.AccountId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonAccountBo>("spPersonAccountGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<PersonAccountBo>();
            }

            return responseBo;
        }
        public ResponseBo<List<PersonAccountListBo>> GetList(PersonAccountGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonAccountListBo>> responseBo = new ResponseBo<List<PersonAccountListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@OwnerPersonId", criteriaBo.OwnerPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@AccountTypeIdList", criteriaBo.AccountTypeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 50);
                    p.Add("@StatId", criteriaBo.StatId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonAccountListBo>("spPersonAccountList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonAccountListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Save(PersonAccountBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@AccountTypeId", saveBo.AccountTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@CurrencyId", saveBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@StatId", saveBo.StatId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@IsFastRetail", saveBo.IsFastRetail, DbType.Boolean, ParameterDirection.Input);

                    conn.Execute("spPersonAccountSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo Delete(PersonAccountDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@AccountId", deleteBo.AccountId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonAccountDel", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, deleteBo);
            }

            return responseBo;
        }

        public ResponseBo<List<PersonAccountActivityListBo>> GetActivityList(PersonAccountActivityGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonAccountActivityListBo>> responseBo = new ResponseBo<List<PersonAccountActivityListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@OwnerPersonId", criteriaBo.OwnerPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@AccountIdList", criteriaBo.AccountIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@ApprovalStatIdList", criteriaBo.ApprovalStatIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 50);

                    p.Add("@IssueDateStart", criteriaBo.IssueDateStart?.Date, DbType.Date, ParameterDirection.Input);
                    p.Add("@IssueDateEnd", criteriaBo.IssueDateEnd?.Date, DbType.Date, ParameterDirection.Input);

                    p.Add("@GrandTotalMin", criteriaBo.GrandTotalMin, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@GrandTotalMax", criteriaBo.GrandTotalMax, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonAccountActivityListBo>("spPersonAccountActivityList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonAccountActivityListBo>>();
            }

            return responseBo;
        }

        public ResponseBo GetFastRetail(PersonAccountGetFastRetailCriteriaBo criteriaBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AccountTypeId", criteriaBo.AccountTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonAccountGetFastRetail", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo);
            }

            return responseBo;
        }
    }
}