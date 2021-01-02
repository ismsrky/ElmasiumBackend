using Dapper;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Product.Category;
using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Product.Price;
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
    public class PersonProductBusiness: BaseBusiness, IPersonProductBusiness
    {
        public ResponseBo<PersonProductBo> Get(PersonProductGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonProductBo> responseBo = new ResponseBo<PersonProductBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonProductId", criteriaBo.PersonProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ProductId", criteriaBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductCode", criteriaBo.ProductCode, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonProductBo>("spPersonProductGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.Bo != null)
                    {
                        if (responseBo.Bo.CodeListRawJson.IsNotNull())
                        {
                            responseBo.Bo.CodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(responseBo.Bo.CodeListRawJson);
                        }

                        if (responseBo.Bo.PriceRawJson.IsNotNull())
                        {
                            responseBo.Bo.Price = JsonConvert.DeserializeObject<ProductPriceBo>(responseBo.Bo.PriceRawJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<PersonProductBo>();
            }

            return responseBo;
        }

        public ResponseBo Delete(PersonProductDeleteBo criteriaBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@Id", criteriaBo.Id, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPersonProductDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<PersonProductListBo>> GetList(PersonProductGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonProductListBo>> responseBo = new ResponseBo<List<PersonProductListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductTypeId", criteriaBo.ProductTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@StockStatId", criteriaBo.StockStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductNameCode", criteriaBo.ProductNameCode, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonProductListBo>("spPersonProductList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.Bo != null)
                    {
                        foreach (PersonProductListBo item in responseBo.Bo)
                        {
                            if (item.PriceRawJson.IsNotNull())
                            {
                                item.Price = JsonConvert.DeserializeObject<ProductPriceBo>(item.PriceRawJson);
                            }

                            if (item.CodeListRawJson.IsNotNull())
                            {
                                item.CodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(item.CodeListRawJson);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonProductListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<PersonProductGeneralBo> GetGeneral(PersonProductGeneralGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonProductGeneralBo> responseBo = new ResponseBo<PersonProductGeneralBo>();
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

                    responseBo.Bo = conn.Query<PersonProductGeneralBo>("spPersonProductGeneralGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<PersonProductGeneralBo>();
            }

            return responseBo;
        }

        public ResponseBo AddToInventory(PersonProductAddInventoryBo addBo)
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

                    p.Add("@PersonId", addBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductId", addBo.ProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", addBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", addBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", addBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonProductAddInventory", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, addBo);
            }

            return responseBo;
        }
        public ResponseBo Update(PersonProductUpdateBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonProductId", updateBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductUpdateTypeListCommaSpr", updateBo.ProductUpdateTypeList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 1000);

                    p.Add("@Name", updateBo.Name, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@CategoryId", updateBo.CategoryId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PurchaseVatRate", updateBo.PurchaseVatRate, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@SaleVatRate", updateBo.SaleVatRate, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@PurhasePrice", updateBo.PurhasePrice, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@SalePrice", updateBo.SalePrice, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@OnlineSalePrice", updateBo.OnlineSalePrice, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@IsTemporarilyUnavaible", updateBo.IsTemporarilyUnavaible, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@IsSaleForOnline", updateBo.IsSaleForOnline, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@Notes", updateBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@MyPersonId", updateBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonProductUpdate", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, updateBo);
            }

            return responseBo;
        }

        public ResponseBo<List<PersonProductActivityListBo>> GetActivityList(PersonProductActivityGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonProductActivityListBo>> responseBo = new ResponseBo<List<PersonProductActivityListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@OwnerPersonId", criteriaBo.OwnerPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductIdList", criteriaBo.ProductIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@ApprovalStatIdList", criteriaBo.ApprovalStatIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 50);

                    p.Add("@IssueDateStart", criteriaBo.IssueDateStart?.Date, DbType.Date, ParameterDirection.Input);
                    p.Add("@IssueDateEnd", criteriaBo.IssueDateEnd?.Date, DbType.Date, ParameterDirection.Input);

                    p.Add("@QuantityTotalMin", criteriaBo.QuantityTotalMin, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@QuantityTotalMax", criteriaBo.QuantityTotalMax, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonProductActivityListBo>("spPersonProductActivityList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonProductActivityListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<PersonProductProfileBo> GetProfile(PersonProductProfileGetCriteriaBo criteriaBo)
        {
            if (criteriaBo.CaseId == 0 && (criteriaBo.PersonUrlName.IsNull() || criteriaBo.PersonUrlName.Trim().Length > 50))
            {
                return new ResponseBo<PersonProductProfileBo>()
                {
                    IsSuccess = false,
                    Message = Stc.GetDicValue("xNoShopFound", criteriaBo.Session.RealPerson.LanguageId)
                };
            }
            else if (criteriaBo.CaseId == 1 && criteriaBo.PersonProductId.IsNull())
            {
                return new ResponseBo<PersonProductProfileBo>()
                {
                    IsSuccess = false,
                    Message = Stc.GetDicValue("xInvalidData", criteriaBo.Session.RealPerson.LanguageId)
                };
            }

            ResponseBo<PersonProductProfileBo> responseBo = new ResponseBo<PersonProductProfileBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@CaseId", criteriaBo.CaseId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonUrlName", criteriaBo.PersonUrlName, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@ProductCode", criteriaBo.ProductCode, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@PersonProductId", criteriaBo.PersonProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonProductProfileBo>("spPersonProductProfileGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.Bo != null)
                    {
                        if (responseBo.Bo.CodeListRawJson.IsNotNull())
                        {
                            responseBo.Bo.CodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(responseBo.Bo.CodeListRawJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<PersonProductProfileBo>();
            }

            return responseBo;
        }

        public ResponseBo<PersonProductSeePriceBo> GetSeePrice(PersonProductSeePriceGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonProductSeePriceBo> responseBo = new ResponseBo<PersonProductSeePriceBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductId", criteriaBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductCode", criteriaBo.ProductCode, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@ShopId", criteriaBo.ShopId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonProductSeePriceBo>("spPersonProductSeePriceGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.Bo != null)
                    {
                        if (responseBo.Bo.CodeListRawJson.IsNotNull())
                        {
                            responseBo.Bo.CodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(responseBo.Bo.CodeListRawJson);
                        }

                        if (responseBo.Bo.PriceRawJson.IsNotNull())
                        {
                            responseBo.Bo.Price = JsonConvert.DeserializeObject<ProductPriceBo>(responseBo.Bo.PriceRawJson);
                            responseBo.Bo.Price.PurhasePrice = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<PersonProductSeePriceBo>();
            }

            return responseBo;
        }

        public ResponseBo<List<ProductCategoryListBo>> GetCategoryList(PersonProductCategoryGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ProductCategoryListBo>> responseBo = new ResponseBo<List<ProductCategoryListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@ProductTypeId", criteriaBo.ProductTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@IsSaleForOnline", criteriaBo.IsSaleForOnline, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@IsTemporarilyUnavaible", criteriaBo.IsTemporarilyUnavaible, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@StockStatId", criteriaBo.StockStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductNameCode", criteriaBo.ProductNameCode, DbType.String, ParameterDirection.Input, 255);

                    responseBo.Bo = conn.Query<ProductCategoryListBo>("spPersonProductCategoryList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ProductCategoryListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<PersonProfileProductListBo>> GetListForProfile(PersonProfileProductGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonProfileProductListBo>> responseBo = new ResponseBo<List<PersonProfileProductListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@PersonProductId", criteriaBo.PersonProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ShopId", criteriaBo.ShopId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@CategoryId", criteriaBo.CategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@StockStatId", criteriaBo.StockStatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@IsSaleForOnline", criteriaBo.IsSaleForOnline, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@IsTemporarilyUnavaible", criteriaBo.IsTemporarilyUnavaible, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@ProductNameCode", criteriaBo.ProductNameCode, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonProfileProductListBo>("spPersonProfileProductList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");                    
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonProfileProductListBo>>();
            }

            return responseBo;
        }
    }
}