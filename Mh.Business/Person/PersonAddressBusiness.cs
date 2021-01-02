using Dapper;
using Mh.Business.Bo.Person.Address;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class PersonAddressBusiness : BaseBusiness, IPersonAddressBusiness
    {
        public ResponseBo<PersonAddressBo> Get(PersonAddressGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonAddressBo> responseBo = new ResponseBo<PersonAddressBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@AddressId", criteriaBo.AddressId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonAddressBo>("spPersonAddressGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<PersonAddressBo>();
            }

            return responseBo;
        }

        public ResponseBo<List<PersonAddressListBo>> GetList(PersonAddressGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonAddressListBo>> responseBo = new ResponseBo<List<PersonAddressListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@OwnerPersonId", criteriaBo.OwnerPersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@AddressTypeIdList", criteriaBo.AddressTypeIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 50);
                    p.Add("@StatId", criteriaBo.StatId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@AddressIdList", criteriaBo.AddressIdList.ToStrSeparated(), DbType.String, ParameterDirection.Input, 255);

                    responseBo.Bo = conn.Query<PersonAddressListBo>("spPersonAddressList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonAddressListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Save(PersonAddressBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@AddressTypeId", saveBo.AddressTypeId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@InvolvedPersonName", saveBo.InvolvedPersonName, DbType.String, ParameterDirection.Input, 510);
                    p.Add("@StatId", saveBo.StatId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@CountryId", saveBo.CountryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@StateId", saveBo.StateId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@CityId", saveBo.CityId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@DistrictId", saveBo.DistrictId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@LocalityId", saveBo.LocalityId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ZipCode", saveBo.ZipCode, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@Phone", saveBo.Phone, DbType.String, ParameterDirection.Input);

                    conn.Execute("spPersonAddressSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo Delete(PersonAddressDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, deleteBo);

                    p.Add("@AddressId", deleteBo.AddressId, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPersonAddressDel", p, commandType: CommandType.StoredProcedure);
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