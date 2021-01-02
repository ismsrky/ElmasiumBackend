using Dapper;
using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Fiche
{
    public class FicheProductBusiness : BaseBusiness, IFicheProductBusiness
    {
        public ResponseBo<List<FicheProductListBo>> GetList(FicheProductGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<FicheProductListBo>> responseBo = new ResponseBo<List<FicheProductListBo>>();
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

                    responseBo.Bo = conn.Query<FicheProductListBo>("spFicheProductList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<FicheProductListBo>>();
            }

            return responseBo;
        }

        public ResponseBo UpdateProducts(FicheBo ficheBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    foreach (FicheProductBo item in ficheBo.ProductList.Where(f => !f.IsDeleted))
                    {
                        item.UnitPrice = item.UnitPrice < 0 ? 0 : Math.Round(item.UnitPrice, 4, MidpointRounding.AwayFromZero);
                        item.VatRate = item.VatRate < 0 ? 0 : Math.Round(item.VatRate, 2, MidpointRounding.AwayFromZero);
                    }

                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@CurrencyId", ficheBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@IsDebtor", ficheBo.DebtPersonId == ficheBo.Session.MyPerson.Id, DbType.Boolean, ParameterDirection.Input);


                    p.Add("@PersonId", ficheBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", ficheBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", ficheBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);


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

                    conn.Execute("spFicheUpdateProducts", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    dtProduct.Dispose();
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, ficheBo);
            }

            return responseBo;
        }
    }
}