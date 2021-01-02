using Dapper;
using Mh.Business.Bo.Address;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Address;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Address
{
    public class AddressBusiness :BaseBusiness, IAddressBusiness
    {
        public ResponseBo<List<AddressCountryBo>> GetCountryList(BaseBo baseBo)
        {
            ResponseBo<List<AddressCountryBo>> responseBo = new ResponseBo<List<AddressCountryBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, baseBo);

                    responseBo.Bo = conn.Query<AddressCountryBo>("spAddressCountryList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<List<AddressCountryBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<AddressStateBo>> GetStateList(AddressGetStateListCriteriaBo criteriaBo)
        {
            ResponseBo<List<AddressStateBo>> responseBo = new ResponseBo<List<AddressStateBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CountryId", criteriaBo.CountryId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<AddressStateBo>("spAddressStateList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<AddressStateBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<AddressCityBo>> GetCityList(AddressGetCityListCriteriaBo criteriaBo)
        {
            ResponseBo<List<AddressCityBo>> responseBo = new ResponseBo<List<AddressCityBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CountryId", criteriaBo.CountryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@StateId", criteriaBo.StateId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<AddressCityBo>("spAddressCityList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<AddressCityBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<AddressDistrictBo>> GetDistrictList(AddressGetDistrictListCriteriaBo criteriaBo)
        {
            ResponseBo<List<AddressDistrictBo>> responseBo = new ResponseBo<List<AddressDistrictBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@CityId", criteriaBo.CityId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<AddressDistrictBo>("spAddressDistrictList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<AddressDistrictBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<AddressLocalityBo>> GetLocalityList(AddressGetLocalityListCriteriaBo criteriaBo)
        {
            ResponseBo<List<AddressLocalityBo>> responseBo = new ResponseBo<List<AddressLocalityBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@DistrictId", criteriaBo.DistrictId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<AddressLocalityBo>("spAddressLocalityList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<AddressLocalityBo>>();
            }

            return responseBo;
        }
    }
}