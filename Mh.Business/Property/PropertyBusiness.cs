using Dapper;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Property;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Property;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Property
{
    public class PropertyBusiness : BaseBusiness, IPropertyBusiness
    {
        public ResponseBo<List<PropertyListBo>> GetList(PropertyGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PropertyListBo>> responseBo = new ResponseBo<List<PropertyListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@CaseId", criteriaBo.CaseId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PersonProductId", criteriaBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PropertyGroupId", criteriaBo.PropertyGroupId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PropertyListBo>("spPropertyList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PropertyListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<PropertyGroupListBo>> GetGroupList(PropertyGroupGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PropertyGroupListBo>> responseBo = new ResponseBo<List<PropertyGroupListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductCategoryId", criteriaBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PropertyGroupListBo>("spPropertyGroupList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PropertyGroupListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Save(PropertySaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductCategoryId", saveBo.ProductCategoryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@PropertyNameListStr", saveBo.PropertyNameListStr, DbType.String, ParameterDirection.Input, int.MaxValue);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPropertySave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }

        public ResponseBo SavePersonProduct(PersonProductPropertyBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonProductId", saveBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PropertyId", saveBo.PropertyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonProductPropertySave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, saveBo);
            }

            return responseBo;
        }
        public ResponseBo DeletePersonProduct(PersonProductPropertyBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonProductId", deleteBo.PersonProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PropertyId", deleteBo.PropertyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonProductPropertyDel", p, commandType: CommandType.StoredProcedure);
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
    }
}