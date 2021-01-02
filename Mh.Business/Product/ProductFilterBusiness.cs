using Dapper;
using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Product.Filter;
using Mh.Business.Bo.Property;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Product
{
    /// <summary>
    /// This business is written to be used in the screen of filtering products that everyone can view.
    /// </summary>
    public class ProductFilterBusiness : BaseBusiness, IProductFilterBusiness
    {
        public ResponseBo<List<ProductFilterListBo>> GetList(ProductFilterGetListCriteriaBo criteriaBo)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResponseBo<List<ProductFilterListBo>> responseBo = new ResponseBo<List<ProductFilterListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@SearchWord", criteriaBo.SearchWord, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PropertyListStr", criteriaBo.PropertyList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@MinPrice", criteriaBo.MinPrice, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@MaxPrice", criteriaBo.MaxPrice, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@PageNumber", criteriaBo.PageNumber, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ApiSessionId", criteriaBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductFilterListBo>("spProductFilterList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ProductFilterListBo>>();
            }

            stopwatch.Stop();
            
            return responseBo;
        }

        public ResponseBo<ProductFilterListExtraBo> GetListExtra(ProductFilterGetListExtraCriteriaBo criteriaBo)
        {
            ResponseBo<ProductFilterListExtraBo> responseBo = new ResponseBo<ProductFilterListExtraBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonProductId", criteriaBo.PersonProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductFilterListExtraBo>("spProductFilterListExtra", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ProductFilterListExtraBo>();
            }

            return responseBo;
        }

        public ResponseBo<ProductFilterListSummaryBo> GetListSummary(ProductFilterGetListCriteriaBo criteriaBo)
        {
            ResponseBo<ProductFilterListSummaryBo> responseBo = new ResponseBo<ProductFilterListSummaryBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@SearchWord", criteriaBo.SearchWord, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PropertyListStr", criteriaBo.PropertyList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@MinPrice", criteriaBo.MinPrice, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@MaxPrice", criteriaBo.MaxPrice, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@ApiSessionId", criteriaBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductFilterListSummaryBo>("spProductFilterListSummary", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.Bo != null && responseBo.Bo.PropertyListJson.IsNotNull())
                    {
                        responseBo.Bo.PropertyList = JsonConvert.DeserializeObject<List<PropertyListBo>>(responseBo.Bo.PropertyListJson);
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ProductFilterListSummaryBo>();
            }

            return responseBo;
        }

        public ResponseBo<List<ProductFilterPoolListBo>> GetPoolList(ProductFilterPoolGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ProductFilterPoolListBo>> responseBo = new ResponseBo<List<ProductFilterPoolListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductNameCode", criteriaBo.ProductNameCode, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@ProductTypeId", criteriaBo.ProductTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OnlyInInventory", criteriaBo.OnlyInInventory, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@OnlyInStock", criteriaBo.OnlyInStock, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductFilterPoolListBo>("spProductFilterPoolList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.Bo != null)
                    {
                        foreach (ProductFilterPoolListBo item in responseBo.Bo)
                        {
                            if (item.ProductCodeListRawJson.IsNull()) continue;

                            item.ProductCodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(item.ProductCodeListRawJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ProductFilterPoolListBo>>();
            }

            return responseBo;
        }
    }
}