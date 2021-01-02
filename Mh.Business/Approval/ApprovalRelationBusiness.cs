using Dapper;
using Mh.Business.Bo.Approval.Relation;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Approval;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Approval
{
    public class ApprovalRelationBusiness : BaseBusiness, IApprovalRelationBusiness
    {
        public ResponseBo MakeRequest(ApprovalRelationRequestBo requestBo)
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

                    p.Add("@ChildRelationTypeId", requestBo.ChildRelationTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ParentPersonId", requestBo.ParentPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ChildPersonId", requestBo.ChildPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@OperatorRealId", requestBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", requestBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spApprovalRelationRequest", p, commandType: CommandType.StoredProcedure);
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
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, requestBo);
            }

            return responseBo;
        }

        public ResponseBo<List<ApprovalRelationListBo>> GetList(ApprovalRelationGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ApprovalRelationListBo>> responseBo = new ResponseBo<List<ApprovalRelationListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@GetIncomings", criteriaBo.GetIncomings, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@MyPersonId", criteriaBo.MyPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ApprovalRelationListBo>("spApprovalRelationList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ApprovalRelationListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Save(ApprovalRelationSaveBo saveBo)
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

                    p.Add("@ApprovalRelationId", saveBo.ApprovalRelationId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonRelationId", saveBo.PersonRelationId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ApprovalStatId", saveBo.ApprovalStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spApprovalRelationSave", p, commandType: CommandType.StoredProcedure);
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
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }
    }
}