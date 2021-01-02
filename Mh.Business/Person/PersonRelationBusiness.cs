using Dapper;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Person.Relation;
using Mh.Business.Bo.Person.Relation.Find;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class PersonRelationBusiness : BaseBusiness, IPersonRelationBusiness
    {
        public ResponseBo<List<PersonRelationListBo>> GetList(PersonRelationGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonRelationListBo>> responseBo = new ResponseBo<List<PersonRelationListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters(); 
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@RelationTypeIdList", criteriaBo.RelationTypeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 255);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@IsOppositeOperation", criteriaBo.IsOppositeOperation, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@SearchRelationTypeOpp", criteriaBo.SearchRelationTypeOpp, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Name", criteriaBo.Name, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonRelationId", criteriaBo.PersonRelationId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@BalanceStatIdList", criteriaBo.BalanceStatIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 50);

                    responseBo.Bo = conn.Query<PersonRelationListBo>("spPersonRelationList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonRelationListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<PersonRelationFindListBo>> GetFindList(PersonRelationFindGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonRelationFindListBo>> responseBo = new ResponseBo<List<PersonRelationFindListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ParentPersonId", criteriaBo.ParentPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Name", criteriaBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@RelationTypeId", criteriaBo.RelationTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonRelationFindListBo>("spPersonRelationFindList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");                   
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonRelationFindListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<Enums.RelationTypes>> GetAvaibleTypeList(PersonRelationAvaibleTypeGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<Enums.RelationTypes>> responseBo = new ResponseBo<List<Enums.RelationTypes>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ChildPersonTypeId", criteriaBo.ChildPersonTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OnlySearchables", criteriaBo.OnlySearchables, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@OnlyMasters", criteriaBo.OnlyMasters, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<Enums.RelationTypes>("spPersonRelationAvaibleTypeList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");                  
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<Enums.RelationTypes>>();
            }

            return responseBo;
        }

        public ResponseBo Delete(PersonRelationDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@NotifyPersonListJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

                    p.Add("@PersonRelationId", deleteBo.PersonRelationId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", deleteBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonRelationDel", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    string NotifyPersonListJson = p.Get<string>("@NotifyPersonListJson");
                    if (NotifyPersonListJson.IsNotNull())
                    {
                        responseBo.PersonNotifyList = JsonConvert.DeserializeObject<List<PersonNotifyListBo>>(NotifyPersonListJson);
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, deleteBo);
            }

            return responseBo;
        }

        public ResponseBo Has(PersonRelationHasCriteriaBo criteriaBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@RelationTypeId", criteriaBo.RelationTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonId1", criteriaBo.PersonId1, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId2", criteriaBo.PersonId2, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPersonRelationHas", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo);
            }

            return responseBo;
        }

        /// <summary>
        /// This is for inside only.
        /// </summary>
        /// <param name="baseBo"></param>
        /// <returns></returns>
        public ResponseBo<List<long>> GetMyPersonIdList(long personId)
        {
            ResponseBo<List<long>> responseBo = new ResponseBo<List<long>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<long>($"select * from dbo.GetMyPersons({personId})").ToList();
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<long>>();
            }

            return responseBo;
        }
    }
}