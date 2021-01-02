using Dapper;
using Mh.Business.Bo.Basket;
using Mh.Business.Bo.Basket.Product;
using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Option;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Product.Code;
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
    public class BasketBusiness : BaseBusiness, IBasketBusiness
    {
        public ResponseBo Delete(BasketDeleteBo deleteBo)
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

                    p.Add("@BasketId", deleteBo.BasketId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ApiSessionId", deleteBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spBasketDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<BasketListBo>> GetList(BasketGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<BasketListBo>> responseBo = new ResponseBo<List<BasketListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@DebtPersonId", criteriaBo.DebtPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@BasketId", criteriaBo.BasketId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@ApiSessionId", criteriaBo.Session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<BasketListBo>("spBasketList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsNotNull() && responseBo.Bo.IsNotNull())
                    {
                        foreach (BasketListBo itemBo in responseBo.Bo)
                        {
                            if (itemBo.CodeListRawJson.IsNotNull())
                            {
                                itemBo.CodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(itemBo.CodeListRawJson);
                            }
                            if (itemBo.OptionListRawJson.IsNotNull())
                            {
                                itemBo.OptionList = JsonConvert.DeserializeObject<List<OptionBo>>(itemBo.OptionListRawJson);
                            }
                            if (itemBo.IncludeExcludeListRawJson.IsNotNull())
                            {
                                itemBo.IncludeExcludeList = JsonConvert.DeserializeObject<List<IncludeExcludeBo>>(itemBo.IncludeExcludeListRawJson);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<BasketListBo>>();
            }

            return responseBo;
        }
    }
}