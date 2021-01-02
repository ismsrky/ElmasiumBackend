using Dapper;
using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Option;
using Mh.Business.Bo.Order;
using Mh.Business.Bo.Person;
using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Order;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Order
{
    public class OrderBusiness : BaseBusiness, IOrderBusiness
    {
        public ResponseBo Save(OrderSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@NotifyPersonListJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

                    p.Add("@BasketId", saveBo.BasketId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@DeliveryAddressId", saveBo.DeliveryAddressId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    conn.Execute("spOrderSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo SaveReturn(OrderReturnSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);
                    p.Add("@NotifyPersonListJson", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

                    p.Add("@OrderId", saveBo.OrderId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    conn.Execute("spOrderSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<OrderListBo>> GetList(OrderGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<OrderListBo>> responseBo = new ResponseBo<List<OrderListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CaseId", criteriaBo.CaseId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@GetIncomings", criteriaBo.GetIncomings, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@GetReturns", criteriaBo.GetReturns, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@OrderStatList", criteriaBo.OrderStatList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 255);

                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@PageOffSet", criteriaBo.PageOffSet, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<OrderListBo>("spOrderList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsNotNull() && responseBo.Bo.IsNotNull())
                    {
                        foreach (OrderListBo itemBo in responseBo.Bo)
                        {
                            if (itemBo.CodeListRawJson.IsNotNull())
                            {
                                itemBo.CodeList = JsonConvert.DeserializeObject<List<ProductCodeBo>>(itemBo.CodeListRawJson);
                            }

                            if (itemBo.OptionListRawJson.IsNotNull())
                            {
                                itemBo.OptionList = JsonConvert.DeserializeObject<List<OptionListBo>>(itemBo.OptionListRawJson);
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
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<OrderListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<OrderStatNextListBo>> GetStatNextList(BaseBo baseBo)
        {
            ResponseBo<List<OrderStatNextListBo>> responseBo = new ResponseBo<List<OrderStatNextListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@MyPersonId", baseBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<OrderStatNextListBo>("spOrderStatNextList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<List<OrderStatNextListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<OrderStatListBo>> GetStatList(BaseBo baseBo)
        {
            ResponseBo<List<OrderStatListBo>> responseBo = new ResponseBo<List<OrderStatListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@MyPersonId", baseBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", baseBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", baseBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<OrderStatListBo>("spOrderStatList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<List<OrderStatListBo>>();
            }

            return responseBo;
        }
    }
}