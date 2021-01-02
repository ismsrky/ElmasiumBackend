using Dapper;
using Mh.Business.Bo.Person.Shop;
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
    public class ShopPersonBusiness : BaseBusiness, IShopPersonBusiness
    {
        public ResponseBo Register(RegisterShopBo registerBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {

                //Enums.Languages langId = registerBo.LanguageId;

                //#region Checks
                //if (registerBo == null)
                //{
                //    result.IsSuccess = false;
                //    result.Message = GetDicValue("xInvalidData", langId);

                //    return result;
                //}
                //if (registerBo.Username.IsNull())
                //{
                //    result.IsSuccess = false;
                //    result.Message = GetDicValue("xEmailCantBeEmpty", langId);

                //    return result;
                //}
                //if (registerBo.Name.IsNull())
                //{
                //    result.IsSuccess = false;
                //    result.Message = GetDicValue("xFirstNameCantBeEmpty", langId);

                //    return result;
                //}
                //if (registerBo.Surname.IsNull())
                //{
                //    result.IsSuccess = false;
                //    result.Message = GetDicValue("xLastNameCantBeEmpty", langId);

                //    return result;
                //}

                //if (registerBo.Password.IsNull())
                //{
                //    result.IsSuccess = false;
                //    result.Message = GetDicValue("xPasswordCantBeEmpty", langId);

                //    return result;
                //}
                //if (Validation.ValidateEmail(registerBo.Username) == false)
                //{
                //    result.IsSuccess = false;
                //    result.Message = GetDicValue("xEnterValidMailAddress", langId);

                //    return result;
                //}
                //#endregion

                //// we may change it later.
                //// but username equals to email.
                //registerBo.Email = registerBo.Username;

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ShortName ", registerBo.ShortName, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@ShopTypeId", registerBo.ShopTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@DefaultCurrencyId", registerBo.DefaultCurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@OperatorRealId", registerBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", registerBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spShopRegister", p, commandType: CommandType.StoredProcedure);
                    responseBo.ReturnedId = p.Get<long?>("@ReturnedId");
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, registerBo);
            }

            return responseBo;
        }

        public ResponseBo FullNameExists(string fullName, Enums.Languages languageId)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@FullName ", fullName, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@PersonTypeId ", (int)Enums.PersonTypes.xShop, DbType.Int32, ParameterDirection.Input);
                    int result = conn.ExecuteScalar("select COUNT(*) from Person P where P.PersonTypeId = @PersonTypeId and P.Surname = @FullName", p).ToInt32();

                    if (result == 0)
                    {
                        responseBo.IsSuccess = true;
                    }
                    else
                    {
                        responseBo.IsSuccess = false;
                        responseBo.Message = GetDicValue("xShopFullNameAlreadyExist", languageId);
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo UpdateGeneral(ShopGeneralBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                if (updateBo.Email.IsNotNull() && Validation.ValidateEmail(updateBo.Email) == false)
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xEnterValidMailAddress", updateBo.Session.RealPerson.LanguageId);

                    return responseBo;
                }

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ShopId", updateBo.ShopId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ShortName ", updateBo.ShortName, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@FullName ", updateBo.FullName, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@ShopTypeId", updateBo.ShopTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@TaxOffice ", updateBo.TaxOffice, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@TaxNumber ", updateBo.TaxNumber, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@UrlName ", updateBo.UrlName, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@Phone ", updateBo.Phone, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@Phone2 ", updateBo.Phone2, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@Email ", updateBo.Email, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@IsIncludingVatPurhasePrice", updateBo.IsIncludingVatPurhasePrice, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@IsIncludingVatSalePrice", updateBo.IsIncludingVatSalePrice, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@DefaultCurrencyId", updateBo.DefaultCurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", updateBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spShopGeneralUpdate", p, commandType: CommandType.StoredProcedure);
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
        public ResponseBo<ShopGeneralBo> GetGeneral(ShopGeneralGetCriteriaBo criteriaBo)
        {
            ResponseBo<ShopGeneralBo> responseBo = new ResponseBo<ShopGeneralBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ShopId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopGeneralBo>("spShopGeneralGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<ShopGeneralBo>();
            }

            return responseBo;
        }

        public ResponseBo UpdateWorkingHours(ShopWorkingHoursBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@MonStartEnd ", updateBo.MonStartEnd, DbType.String, ParameterDirection.Input, 8);
                    p.Add("@TuesStartEnd ", updateBo.TuesStartEnd, DbType.String, ParameterDirection.Input, 8);
                    p.Add("@WedStartEnd ", updateBo.WedStartEnd, DbType.String, ParameterDirection.Input, 8);
                    p.Add("@ThursStartEnd ", updateBo.ThursStartEnd, DbType.String, ParameterDirection.Input, 8);
                    p.Add("@FriStartEnd ", updateBo.FriStartEnd, DbType.String, ParameterDirection.Input, 8);
                    p.Add("@SatStartEnd ", updateBo.SatStartEnd, DbType.String, ParameterDirection.Input, 8);
                    p.Add("@SunStartEnd ", updateBo.SunStartEnd, DbType.String, ParameterDirection.Input, 8);

                    p.Add("@TakesOrderOutTime ", updateBo.TakesOrderOutTime, DbType.Boolean, ParameterDirection.Input);

                    p.Add("@ShopId", updateBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spShopWorkingHoursUpdate", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<ShopWorkingHoursBo> GetWorkingHours(ShopWorkingHoursGetCriteriaBo criteriaBo)
        {
            ResponseBo<ShopWorkingHoursBo> responseBo = new ResponseBo<ShopWorkingHoursBo>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ShopId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopWorkingHoursBo>("spShopWorkingHoursGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ShopWorkingHoursBo>();
            }

            return responseBo;
        }

        public ResponseBo<ShopProfileBo> GetProfile(ShopProfileGetCriteriaBo criteriaBo)
        {
            if (criteriaBo.UrlName.IsNull() || criteriaBo.UrlName.Trim().Length > 50)
            {
                return new ResponseBo<ShopProfileBo>()
                {
                    IsSuccess = false,
                    Message = Stc.GetDicValue("xNoShopFound", criteriaBo.Session.RealPerson.LanguageId)
                };
            }

            ResponseBo<ShopProfileBo> responseBo = new ResponseBo<ShopProfileBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@UrlName", criteriaBo.UrlName, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopProfileBo>("spShopProfileGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsSuccess && responseBo.Bo != null)
                    {
                        if (responseBo.Bo.OrderAccountListRawJson.IsNotNull())
                        {
                            responseBo.Bo.OrderAccountList = JsonConvert.DeserializeObject<List<Enums.AccountTypes>>(responseBo.Bo.OrderAccountListRawJson);
                        }

                        if (responseBo.Bo.OrderCurrencyListRawJson.IsNotNull())
                        {
                            responseBo.Bo.OrderCurrencyList = JsonConvert.DeserializeObject<List<Enums.Currencies>>(responseBo.Bo.OrderCurrencyListRawJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ShopProfileBo>();
            }

            return responseBo;
        }

        public ResponseBo UpdateOrderGeneral(ShopOrderGeneralBo updateBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                string OrderAccountListRawJson = null;
                if (updateBo.OrderAccountList != null && updateBo.OrderAccountList.Count() > 0)
                {
                    OrderAccountListRawJson = JsonConvert.SerializeObject(updateBo.OrderAccountList);
                }

                string OrderCurrencyListRawJson = null;
                if (updateBo.OrderCurrencyList != null && updateBo.OrderCurrencyList.Count() > 0)
                {
                    OrderCurrencyListRawJson = JsonConvert.SerializeObject(updateBo.OrderCurrencyList);
                }

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@TakesOrder", updateBo.TakesOrder, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@OrderAccountListRawJson", OrderAccountListRawJson, DbType.String, ParameterDirection.Input, 4000);
                    p.Add("@OrderCurrencyListRawJson", OrderCurrencyListRawJson, DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@ShopId", updateBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", updateBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", updateBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spShopOrderUpdate", p, commandType: CommandType.StoredProcedure);
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
        public ResponseBo<ShopOrderGeneralBo> GetOrderGeneral(ShopOrderGeneralGetCriteriaBo criteriaBo)
        {
            ResponseBo<ShopOrderGeneralBo> responseBo = new ResponseBo<ShopOrderGeneralBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@ShopId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopOrderGeneralBo>("spShopOrderGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsSuccess && responseBo.Bo != null)
                    {
                        if (responseBo.Bo.OrderAccountListRawJson.IsNotNull())
                        {
                            responseBo.Bo.OrderAccountList = JsonConvert.DeserializeObject<List<Enums.AccountTypes>>(responseBo.Bo.OrderAccountListRawJson);
                        }

                        if (responseBo.Bo.OrderCurrencyListRawJson.IsNotNull())
                        {
                            responseBo.Bo.OrderCurrencyList = JsonConvert.DeserializeObject<List<Enums.Currencies>>(responseBo.Bo.OrderCurrencyListRawJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ShopOrderGeneralBo>();
            }

            return responseBo;
        }

        public ResponseBo SaveOrderArea(ShopOrderAreaBo saveBo)
        {
            if (saveBo == null || saveBo.SubList == null || saveBo.SubList.Count == 0)
            {
                return new ResponseBo()
                {
                    IsSuccess = false,
                    Message = Stc.GetDicValue("xInvalidData", saveBo.Session.RealPerson.LanguageId)
                };
            }

            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@ReturnedId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@AddressBoundaryId", saveBo.AddressBoundaryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressCountryId", saveBo.AddressCountryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressStateId", saveBo.AddressStateId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressCityId", saveBo.AddressCityId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressDistrictId", saveBo.AddressDistrictId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@CurrencyId", saveBo.CurrencyId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@OrderDeliveryTypeId", saveBo.OrderDeliveryTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@SubListRawJson", JsonConvert.SerializeObject(saveBo.SubList), DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@MyPersonId", saveBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", saveBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", saveBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spPersonOrderAreaSave", p, commandType: CommandType.StoredProcedure);
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
        public ResponseBo<List<ShopOrderAreaListBo>> GetOrderAreaList(ShopOrderAreaGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ShopOrderAreaListBo>> responseBo = new ResponseBo<List<ShopOrderAreaListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopOrderAreaListBo>("spPersonOrderAreaList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsSuccess && responseBo.Bo != null && responseBo.Bo.Count() > 0)
                    {
                        foreach (ShopOrderAreaListBo item in responseBo.Bo)
                        {
                            // 'SubListRawJson' cannot be null.
                            item.SubList = JsonConvert.DeserializeObject<List<ShopOrderAreaSubListBo>>(item.SubListRawJson);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ShopOrderAreaListBo>>();
            }

            return responseBo;
        }
        public ResponseBo<ShopOrderAreaBo> GetOrderArea(ShopOrderAreaGetCriteriaBo criteriaBo)
        {
            ResponseBo<ShopOrderAreaBo> responseBo = new ResponseBo<ShopOrderAreaBo>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@Id", criteriaBo.Id, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopOrderAreaBo>("spPersonOrderAreaGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    if (responseBo.IsSuccess && responseBo.Bo != null)
                    {
                        // 'SubListRawJson' cannot be null.
                        responseBo.Bo.SubList = JsonConvert.DeserializeObject<List<ShopOrderAreaSubBo>>(responseBo.Bo.SubListRawJson);
                    }
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ShopOrderAreaBo>();
            }

            return responseBo;
        }
        public ResponseBo DeleteOrderArea(ShopOrderAreaDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonOrderAreaId", deleteBo.PersonOrderAreaId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", deleteBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonOrderAreaDel", p, commandType: CommandType.StoredProcedure);
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
        public ResponseBo DeleteOrderAreaSub(ShopOrderAreaSubDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonOrderAreaSubId", deleteBo.PersonOrderAreaSubId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", deleteBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@MyPersonId", deleteBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", deleteBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", deleteBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spPersonOrderAreaSubDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<ShopOrderAccountListBo>> GetOrderAccountList(ShopOrderAccountGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<ShopOrderAccountListBo>> responseBo = new ResponseBo<List<ShopOrderAccountListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);
                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<ShopOrderAccountListBo>("spPersonOrderAccountList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");                    
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<ShopOrderAccountListBo>>();
            }

            return responseBo;
        }
    }
}