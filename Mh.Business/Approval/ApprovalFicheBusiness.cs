using Dapper;
using Mh.Business.Bo.Approval.Fiche;
using Mh.Business.Bo.Fiche.Money;
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
    public class ApprovalFicheBusiness : BaseBusiness, IApprovalFicheBusiness
    {
        public ResponseBo<List<ApprovalFicheListBo>> GetList(ApprovalFicheGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ApprovalFicheListBo>> responseBo = new ResponseBo<List<ApprovalFicheListBo>>();
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

                    responseBo.Bo = conn.Query<ApprovalFicheListBo>("spApprovalFicheList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ApprovalFicheListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Save(ApprovalFicheSaveBo saveBo)
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
                    p.Add("@NotifyPersonListJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

                    p.Add("@Inner", false, DbType.Boolean, ParameterDirection.Input); // true means called from another 'spApprovalFicheSave'

                    p.Add("@FicheId", saveBo.FicheId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ApprovalStatId", saveBo.ApprovalStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id", typeof(long));
                    dt.Columns.Add("DebtPersonAccountId", typeof(long));
                    dt.Columns.Add("CreditPersonAccountId", typeof(long));
                    dt.Columns.Add("Total", typeof(decimal));
                    dt.Columns.Add("DebtPersonAccountTypeId", typeof(int));
                    dt.Columns.Add("CreditPersonAccountTypeId", typeof(int));
                    dt.Columns.Add("Notes", typeof(string));

                    if (saveBo.ChoiceReturnList != null && saveBo.ChoiceReturnList.Count > 0)
                    {
                        foreach (FicheMoneyBo item in saveBo.ChoiceReturnList)
                        {
                            DataRow dr = dt.NewRow();
                            dr["Id"] = item.Id;
                            if (item.DebtPersonAccountId == null)
                                dr["DebtPersonAccountId"] = DBNull.Value;
                            else
                                dr["DebtPersonAccountId"] = item.DebtPersonAccountId;

                            if (item.CreditPersonAccountId == null)
                                dr["CreditPersonAccountId"] = DBNull.Value;
                            else
                                dr["CreditPersonAccountId"] = item.CreditPersonAccountId;

                            dr["Total"] = item.Total;
                            dr["DebtPersonAccountTypeId"] = item.DebtPersonAccountTypeId;
                            dr["CreditPersonAccountTypeId"] = item.CreditPersonAccountTypeId;
                            dr["Notes"] = item.Notes;

                            dt.Rows.Add(dr);
                        }

                        p.Add("@tvpChoiceReturnList", dt.AsTableValuedParameter("tvpFicheMoney"));
                    }

                    conn.Execute("spApprovalFicheSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long>("@ReturnedId");

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