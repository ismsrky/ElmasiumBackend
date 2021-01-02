using Dapper;
using Mh.Business.Bo.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Mh.Business.Product
{
    public class ProductBusiness : BaseBusiness, IProductBusiness
    {
        public ResponseBo Save(ProductSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@ProductTypeId", saveBo.ProductTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@SalePrice", saveBo.SalePrice, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@PurhasePrice", saveBo.PurhasePrice, DbType.Decimal, ParameterDirection.Input);
                    p.Add("@CurrencyId", saveBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@VatRate", saveBo.VatRate, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@Barcode", saveBo.Barcode, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@RefSourceJson", saveBo.RefSourceJson, DbType.String, ParameterDirection.Input, Int32.MaxValue);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spProductSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo Update(ProductUpdateBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductId", updateBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductUpdateTypeId", updateBo.ProductUpdateTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Name", updateBo.Name, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@BrandId", null, DbType.Int32, ParameterDirection.Input);//delete this
                    p.Add("@OriginCountryId", updateBo.OriginCountryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Notes", updateBo.Notes, DbType.String, ParameterDirection.Input, 4000);
                    p.Add("@CategoryId", updateBo.CategoryId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ImageUniqueId", updateBo.ImageUniqueId, DbType.Guid, ParameterDirection.Input);
                    p.Add("@ImageFileTypeId", updateBo.ImageFileTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spProductUpdate", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo UpdateCheck(ProductUpdateCheckBo updateCheckBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ProductId", updateCheckBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ProductUpdateTypeId", updateCheckBo.ProductUpdateTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@LanguageId", updateCheckBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateCheckBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spProductUpdateCheck", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, updateCheckBo);
            }

            return responseBo;
        }

        public ResponseBo SaveStar(ProductStarBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ProductId", saveBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Star", saveBo.Star, DbType.Byte, ParameterDirection.Input);

                    conn.Execute("spProductStarSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo GetNextId()
        {
            return base.GetNextId("seProduct");
        }

        /// <summary>
        /// This method written for project 'ImageImport'.
        /// So do not add this to the interface.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public long? GetProductIdFromCode(string code)
        {
            long? productId = null;

            using (SqlConnection conn = DbAccess.Connection.GetConn())
            {
                productId = conn.ExecuteScalar<long?>("select PC.ProductId from ProductCode PC where PC.Code = '" + code + "' and (select COUNT(*) from [Image] PIM where PIM.ProductId = PC.ProductId and PIM.ImageTypeId = 0) = 0", commandType: CommandType.Text);
            }

            return productId;
        }
    }
}