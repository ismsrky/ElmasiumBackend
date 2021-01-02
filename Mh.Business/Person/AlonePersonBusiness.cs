using Dapper;
using Mh.Business.Bo.Person.Alone;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class AlonePersonBusiness : BaseBusiness, IAlonePersonBusiness
    {
        public ResponseBo<AlonePersonBo> Get(AlonePersonGetCriteriaBo criteriaBo)
        {
            ResponseBo<AlonePersonBo> responseBo = new ResponseBo<AlonePersonBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ParentRelationPersonId", criteriaBo.ParentRelationPersonId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<AlonePersonBo>("spAloneGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<AlonePersonBo>();
            }

            return responseBo;
        }

        public ResponseBo Save(AlonePersonBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input,50);
                    p.Add("@Surname", saveBo.Surname, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@Email", saveBo.Email, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@PersonTypeId", saveBo.PersonTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@StatId", saveBo.StatId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@DefaultCurrencyId", saveBo.DefaultCurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@Phone", saveBo.Phone, DbType.String, ParameterDirection.Input);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@TaxOffice ", saveBo.TaxOffice, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@TaxNumber ", saveBo.TaxNumber, DbType.String, ParameterDirection.Input, 50);

                    p.Add("@AddressCityId", saveBo.AddressCityId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressDistrictId", saveBo.AddressDistrictId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressCountryId", saveBo.AddressCountryId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressStateId", saveBo.AddressStateId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@AddressNotes ", saveBo.AddressNotes, DbType.String, ParameterDirection.Input, 255);

                    p.Add("@ParentRelationPersonId", saveBo.ParentRelationPersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@ChildRelationTypeId", saveBo.ChildRelationTypeId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spAloneSave", p, commandType: CommandType.StoredProcedure);
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
    }
}