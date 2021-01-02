using Dapper;
using Mh.Business.Bo.Product.Category;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Product
{
    public class ProductCategoryBusiness : BaseBusiness, IProductCategoryBusiness
    {
        public ResponseBo<List<ProductCategoryListBo>> GetList(ProductCategoryGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ProductCategoryListBo>> responseBo = new ResponseBo<List<ProductCategoryListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@IsUpper", criteriaBo.IsUpper, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductCategoryListBo>("spProductCategoryList", p, commandType: CommandType.StoredProcedure).ToList();
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

        public ResponseBo<List<ProductCategoryListAdminBo>> GetListAdmin(ProductCategoryGetListAdminCriteriaBo criteriaBo)
        {
            ResponseBo<List<ProductCategoryListAdminBo>> responseBo = new ResponseBo<List<ProductCategoryListAdminBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ParentId", criteriaBo.ParentId, DbType.Int32, ParameterDirection.Input);

                    //p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductCategoryListAdminBo>("spProductCategoryListAdmin", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ProductCategoryListAdminBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<ProductCategoryListBo>> GetListAdmin(ProductCategoryGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ProductCategoryListBo>> responseBo = new ResponseBo<List<ProductCategoryListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ParentId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductCategoryListBo>("spProductCategoryList", p, commandType: CommandType.StoredProcedure).ToList();
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

        public ResponseBo Save(ProductCategoryListBo saveBo)
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

                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@Id", saveBo.Id, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@ParentId", saveBo.ParentId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spProductCategorySave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo Delete(ProductCategoryListBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, deleteBo);

                    p.Add("@Id", deleteBo.Id, DbType.Int32, ParameterDirection.Input);
                    
                    conn.Execute("spProductCategoryDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<ProductCategoryCheckUrlBo> CheckUrl(ProductCategoryCheckUrlCriteriaBo criteriaBo)
        {
            ResponseBo<ProductCategoryCheckUrlBo> responseBo = new ResponseBo<ProductCategoryCheckUrlBo>();

            List<dynamic> dList = new List<dynamic>();
            for (int i = 0; i < criteriaBo.UrlSegmentList.Count(); i++)
            {
                dynamic dItem = new ExpandoObject();
                dItem.Segment = criteriaBo.UrlSegmentList[i];
                dItem.Index = i;
                dList.Add(dItem);
            }

            string UrlSegmentListRawJson = JsonConvert.SerializeObject(dList);

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@UrlSegmentListRawJson", UrlSegmentListRawJson, DbType.String, ParameterDirection.Input, 1000);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ProductCategoryCheckUrlBo>("spProductCategoryCheckUrl", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ProductCategoryCheckUrlBo>();
            }

            return responseBo;
        }
    }
}