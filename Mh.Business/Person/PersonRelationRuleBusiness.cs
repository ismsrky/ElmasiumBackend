using Dapper;
using Mh.Business.Bo.Person.Relation;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Person
{
    public class PersonRelationRuleBusiness : BaseBusiness, IPersonRelationRuleBusiness
    {
        public ResponseBo<List<PersonRelationRuleListBo>> GetList(PersonRelationRuleGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonRelationRuleListBo>> responseBo = new ResponseBo<List<PersonRelationRuleListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonRelationRuleListBo>("spPersonRelationRuleList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonRelationRuleListBo>>();
            }

            return responseBo;
        }
    }
}