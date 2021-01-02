using Dapper;
using Mh.Business.Bo.Basket.Product;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Basket;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Basket
{
    public class BasketProductBusiness : BaseBusiness, IBasketProductBusiness
    {
        public ResponseBo Save(BasketProductSaveBo saveBo)
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

                    p.Add("@DebtPersonId", saveBo.DebtPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@CreditPersonId", saveBo.CreditPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ProductId", saveBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Quantity", saveBo.Quantity, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@CurrencyId", saveBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@IsFastSale", saveBo.IsFastSale, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@OptionIdListCommaSpr", saveBo.OptionIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 1000);
                    p.Add("@IEIdListCommaSpr", saveBo.IncludeExcludeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 1000);

                    p.Add("@ApiSessionId", saveBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spBasketProductSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");

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

        public ResponseBo UpdateQuantity(BasketProductQuantityUpdateBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@BasketProductId", updateBo.BasketProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Quantity", updateBo.Quantity, DbType.Decimal, ParameterDirection.Input);

                    p.Add("@ApiSessionId", updateBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", updateBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spBasketProductQuantityUpdate", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo Delete(BasketProductDeleteBo deleteBo)
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

                    p.Add("@BasketProductId", deleteBo.BasketProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ApiSessionId", deleteBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spBasketProductDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo UpdateOption(BasketProductOptionUpdateBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@BasketProductId", updateBo.BasketProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OptionIdListCommaSpr", updateBo.OptionIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 1000);

                    p.Add("@ApiSessionId", updateBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", updateBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spBasketProductOptionUpdate", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo UpdateIncludeExclude(BasketProductIncludeExcludeUpdateBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@BasketProductId", updateBo.BasketProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@IEIdListCommaSpr", updateBo.IncludeExcludeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 1000);

                    p.Add("@ApiSessionId", updateBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", updateBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spBasketProductIncludeExcludeUpdate", p, commandType: CommandType.StoredProcedure);
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
    }
}