using Dapper;
using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Fiche.Product;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Fiche
{
    public class FicheBusiness : BaseBusiness, IFicheBusiness
    {
        public ResponseBo Save(FicheBo ficheBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    ficheBo.RowDiscountTotal = 0;

                    if (ficheBo.FicheTypeId != Enums.FicheTypes.xDebtCredit)
                    {
                        ficheBo.GrandTotal = 0;
                        ficheBo.Total = 0;
                    }

                    if (ficheBo.FicheTypeId == Enums.FicheTypes.xMoneyTransfer
                        || ficheBo.FicheTypeId == Enums.FicheTypes.xPayment)
                    {
                        ficheBo.UnderDiscountTotal = 0;
                        ficheBo.ProductList = null;
                        ficheBo.VatTotalList = null;

                        ficheBo.GrandTotal = ficheBo.MoneyList.Sum(x => x.Total);
                        ficheBo.Total = ficheBo.GrandTotal;
                    }
                    else  if(ficheBo.FicheTypeId == Enums.FicheTypes.xDebtCredit)
                    {
                        ficheBo.UnderDiscountTotal = 0;
                        ficheBo.ProductList = null;
                        ficheBo.VatTotalList = null;
                        ficheBo.MoneyList = null;

                        ficheBo.Total = ficheBo.GrandTotal;
                    }
                    else if (ficheBo.FicheTypeId == Enums.FicheTypes.xReceipt || ficheBo.FicheTypeId == Enums.FicheTypes.xInvoice)
                    {
                        ficheBo.VatTotalList = new List<FicheVatTotalBo>();
                        foreach (FicheProductBo item in ficheBo.ProductList.Where(f => !f.IsDeleted))
                        {
                            item.Quantity = item.Quantity <= 0 ? 1 : Math.Round(item.Quantity, 4, MidpointRounding.AwayFromZero);
                            item.UnitPrice = item.UnitPrice < 0 ? 0 : Math.Round(item.UnitPrice, 4, MidpointRounding.AwayFromZero);
                            item.DiscountRate = item.DiscountRate < 0 ? 0 : Math.Round(item.DiscountRate, 2, MidpointRounding.AwayFromZero);
                            item.VatRate = item.VatRate < 0 ? 0 : Math.Round(item.VatRate, 2, MidpointRounding.AwayFromZero);

                            item.Total = item.Quantity * item.UnitPrice;

                            item.VatTotal = ficheBo.IncludingVat ?
                                item.Total - item.Total / (1 + item.VatRate / 100) :
                                Math.Round(item.Total / (decimal)100 * item.VatRate, 4, MidpointRounding.AwayFromZero);

                            item.DiscountTotal = Math.Round((item.Total + (ficheBo.IncludingVat ? 0 : item.VatTotal)) / 100 * item.DiscountRate, 2, MidpointRounding.AwayFromZero);
                            item.GrandTotal = item.Total + (ficheBo.IncludingVat ? 0 : item.VatTotal) - item.DiscountTotal;

                            if (item.GrandTotal < 0)
                            {
                                responseBo.IsSuccess = false;
                                responseBo.Message = "Satır genel toplamı 0'dan küçük olamaz.";
                                return responseBo;
                            }

                            ficheBo.RowDiscountTotal += item.DiscountTotal;
                            ficheBo.Total += item.Total;
                            ficheBo.GrandTotal += item.GrandTotal;

                            // Calculating vat totals.
                            if (item.VatRate > 0 && item.VatTotal > 0) // we do not want to see 0 value :)
                            {
                                if (ficheBo.VatTotalList.Count(v => v.VatRate == item.VatRate) == 0)
                                {
                                    ficheBo.VatTotalList.Add(new FicheVatTotalBo()
                                    {
                                        VatRate = item.VatRate,
                                        VatTotal = 0
                                    });
                                }
                                ficheBo.VatTotalList.Find(v => v.VatRate == item.VatRate).VatTotal += item.VatTotal;
                            }
                        }


                        ficheBo.UnderDiscountTotal = Math.Round(ficheBo.GrandTotal / 100 * ficheBo.UnderDiscountRate, 2, MidpointRounding.AwayFromZero);

                        ficheBo.GrandTotal -= ficheBo.UnderDiscountTotal;                       
                    }

                    if (ficheBo.GrandTotal < 0)
                    {
                        responseBo.IsSuccess = false;
                        responseBo.Message = "Genel toplam 0'dan küçük olamaz.";
                        return responseBo;
                    }

                    if ((ficheBo.FicheTypeId == Enums.FicheTypes.xMoneyTransfer
                        || ficheBo.FicheTypeId == Enums.FicheTypes.xPayment
                        || ficheBo.FicheTypeId == Enums.FicheTypes.xDebtCredit)
                        && ficheBo.GrandTotal == 0)
                    {
                        responseBo.IsSuccess = false;
                        responseBo.Message = "Genel toplam 0'dan büyük olmalı.";
                        return responseBo;
                    }

                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@NotifyPersonListJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

                    p.Add("@FicheId", ficheBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@DebtPersonId", ficheBo.DebtPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CreditPersonId", ficheBo.CreditPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@FicheTypeId", ficheBo.FicheTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@FicheContentId", ficheBo.FicheContentId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@FicheContentGroupId", ficheBo.FicheContentGroupId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@CurrencyId", ficheBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PrintedCode", ficheBo.PrintedCode, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@IncludingVat", ficheBo.IncludingVat, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@IssueDate", ficheBo.IssueDate, DbType.Date, ParameterDirection.Input);
                    p.Add("@DueDate", ficheBo.DueDate, DbType.Date, ParameterDirection.Input);

                    p.Add("@UnderDiscountRate", ficheBo.UnderDiscountRate, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@UnderDiscountTotal", ficheBo.UnderDiscountTotal, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@GrandTotal", ficheBo.GrandTotal, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@RowDiscountTotal", ficheBo.RowDiscountTotal, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@Total", ficheBo.Total, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@Notes", ficheBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@AcceptorPersonId", ficheBo.AcceptorPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@IsUncompleted", ficheBo.IsUncompleted, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@OrderId", ficheBo.OrderId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", ficheBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", ficheBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", ficheBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    List<long> PaymentTypeFicheRelationChildIdList = null;
                    if (ficheBo.RelationList != null)
                    {
                        if (ficheBo.RelationList.Count(x => x.FicheRelationTypeId == Enums.FicheRelationTypes.xPayment) > 0)
                        {
                            PaymentTypeFicheRelationChildIdList = (from y in ficheBo.RelationList
                                                                   where y.FicheRelationTypeId == Enums.FicheRelationTypes.xPayment
                                                                   select y.ChildFicheId).ToList();

                        }
                    }
                    p.Add("@PaymentTypeFicheRelationChildIdList", PaymentTypeFicheRelationChildIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    DataTable dtMoney = new DataTable();
                    dtMoney.Columns.Add("Id", typeof(long));
                    dtMoney.Columns.Add("DebtPersonAccountId", typeof(long));
                    dtMoney.Columns.Add("CreditPersonAccountId", typeof(long));
                    dtMoney.Columns.Add("Total", typeof(decimal));
                    dtMoney.Columns.Add("DebtPersonAccountTypeId", typeof(int));
                    dtMoney.Columns.Add("CreditPersonAccountTypeId", typeof(int));
                    dtMoney.Columns.Add("Notes", typeof(string));
                    if (ficheBo.MoneyList != null && ficheBo.MoneyList.Count > 0)
                    {
                        foreach (FicheMoneyBo item in ficheBo.MoneyList)
                        {
                            DataRow dr = dtMoney.NewRow();
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

                            dtMoney.Rows.Add(dr);
                        }

                        p.Add("@tvpFicheMoneyList", dtMoney.AsTableValuedParameter("tvpFicheMoney"));
                    }


                    DataTable dtProduct = new DataTable();
                    dtProduct.Columns.Add("Id", typeof(long));
                    dtProduct.Columns.Add("ProductId", typeof(long));
                    dtProduct.Columns.Add("Quantity", typeof(decimal));
                    dtProduct.Columns.Add("UnitPrice", typeof(decimal));
                    dtProduct.Columns.Add("DiscountRate", typeof(decimal));
                    dtProduct.Columns.Add("DiscountTotal", typeof(decimal));
                    dtProduct.Columns.Add("VatRate", typeof(decimal));
                    dtProduct.Columns.Add("Notes", typeof(string));
                    dtProduct.Columns.Add("Total", typeof(decimal));
                    dtProduct.Columns.Add("VatTotal", typeof(decimal));
                    dtProduct.Columns.Add("GrandTotal", typeof(decimal));
                    dtProduct.Columns.Add("IsDeleted", typeof(bool));
                    if (ficheBo.ProductList != null && ficheBo.ProductList.Count > 0)
                    {
                        foreach (FicheProductBo item in ficheBo.ProductList)
                        {
                            DataRow dr = dtProduct.NewRow();
                            dr["Id"] = item.Id;
                            dr["ProductId"] = item.ProductId;
                            dr["Quantity"] = item.Quantity;
                            dr["UnitPrice"] = item.UnitPrice;
                            dr["DiscountRate"] = item.DiscountRate;
                            dr["DiscountTotal"] = item.DiscountTotal;
                            dr["VatRate"] = item.VatRate;
                            dr["Notes"] = item.Notes;
                            dr["Total"] = item.Total;
                            dr["VatTotal"] = item.VatTotal;
                            dr["GrandTotal"] = item.GrandTotal;
                            dr["IsDeleted"] = item.IsDeleted;

                            dtProduct.Rows.Add(dr);
                        }

                        p.Add("@tvpFicheProductList", dtProduct.AsTableValuedParameter("tvpFicheProduct"));
                    }

                    DataTable dtVatTotal = new DataTable();
                    dtVatTotal.Columns.Add("VatRate", typeof(decimal));
                    dtVatTotal.Columns.Add("VatTotal", typeof(decimal));
                    if (ficheBo.VatTotalList != null && ficheBo.VatTotalList.Count > 0)
                    {
                        foreach (FicheVatTotalBo item in ficheBo.VatTotalList)
                        {
                            DataRow dr = dtVatTotal.NewRow();
                            dr["VatRate"] = item.VatRate;
                            dr["VatTotal"] = item.VatTotal;

                            dtVatTotal.Rows.Add(dr);
                        }

                        p.Add("@tvpFicheVatTotal", dtVatTotal.AsTableValuedParameter("tvpFicheVatTotal"));
                    }

                    conn.Execute("spFicheSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    string NotifyPersonListJson = p.Get<string>("@NotifyPersonListJson");
                    if (NotifyPersonListJson.IsNotNull())
                    {
                        responseBo.PersonNotifyList = JsonConvert.DeserializeObject<List<PersonNotifyListBo>>(NotifyPersonListJson);
                    }

                    dtMoney.Dispose();
                    dtProduct.Dispose();
                    dtVatTotal.Dispose();
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, ficheBo);
            }

            return responseBo;
        }

        public ResponseBo<FicheBo> Get(FicheGetCriteriaBo criteriaBo)
        {
            ResponseBo<FicheBo> responseBo = new ResponseBo<FicheBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@FicheJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

                    p.Add("@MyPersonId", criteriaBo.MyPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@FicheId", criteriaBo.FicheId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spFicheGet", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    string ficheJson = p.Get<string>("@FicheJson");

                    responseBo.Bo = JsonConvert.DeserializeObject<FicheBo>(ficheJson);
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<FicheBo>();
            }

            return responseBo;
        }

        public ResponseBo<List<FicheListBo>> GetList(FicheGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<FicheListBo>> responseBo = new ResponseBo<List<FicheListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@ApprovalStatIdList", criteriaBo.ApprovalStatIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 255);
                    p.Add("@FicheContentIdList", criteriaBo.FicheContentIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@IssueDateStart", criteriaBo.IssueDateStart?.Date, DbType.Date, ParameterDirection.Input);
                    p.Add("@IssueDateEnd", criteriaBo.IssueDateEnd?.Date, DbType.Date, ParameterDirection.Input);

                    p.Add("@PrintedCode", criteriaBo.PrintedCode, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@GrandTotalMin", criteriaBo.GrandTotalMin, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@GrandTotalMax", criteriaBo.GrandTotalMax, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PaymentStatId", criteriaBo.PaymentStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@FicheId", criteriaBo.FicheId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@FicheIdRelated", criteriaBo.FicheIdRelated, DbType.Int64, ParameterDirection.Input);

                    p.Add("@DebtPersonId", criteriaBo.DebtPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CreditPersonId", criteriaBo.CreditPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OtherPersonsIdList", criteriaBo.OtherPersonsIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@FicheTypeFakeIdList", criteriaBo.FicheTypeFakeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 255);

                    p.Add("@ExcludingFicheIdList", criteriaBo.ExcludingFicheIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    responseBo.Bo = conn.Query<FicheListBo>("spFicheList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<FicheListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Delete(FicheDeleteBo deleteBo)
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

                    p.Add("@FicheId", deleteBo.FicheId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@Inner", false, DbType.Boolean, ParameterDirection.Input); // true means called inside db.

                    conn.Execute("spFicheDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<FicheApprovalHistoryListBo>> GetApprovalHistoryList(FicheApprovalHistoryGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<FicheApprovalHistoryListBo>> responseBo = new ResponseBo<List<FicheApprovalHistoryListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@FicheId", criteriaBo.FicheId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<FicheApprovalHistoryListBo>("spFicheApprovalHistoryList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<FicheApprovalHistoryListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<FicheActionsBo> GetActions(FicheGetActionsCriteriaBo criteriaBo)
        {
            ResponseBo<FicheActionsBo> responseBo = new ResponseBo<FicheActionsBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@FicheId", criteriaBo.FicheId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    
                    responseBo.Bo = conn.Query<FicheActionsBo>("spFicheActionsGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<FicheActionsBo>();
            }

            return responseBo;
        }

        public ResponseBo GetNextId()
        {
            return base.GetNextId("seFiche");
        }
    }
}