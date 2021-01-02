using Dapper;
using Mh.Business.Bo.Person.Table;
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
    public class PersonTableBusiness : BaseBusiness, IPersonTableBusiness
    {
        public ResponseBo Save(PersonTableBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@GroupId", saveBo.GroupId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@PersonTableStatId", saveBo.PersonTableStatId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Order", saveBo.Order, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    conn.Execute("spPersonTableSave", p, commandType: CommandType.StoredProcedure);
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
        public ResponseBo<PersonTableBo> Get(PersonTableGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonTableBo> responseBo = new ResponseBo<PersonTableBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@Id", criteriaBo.Id, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonTableBo>("spPersonTableGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<PersonTableBo>();
            }

            return responseBo;
        }
        public ResponseBo Delete(PersonTableDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, deleteBo);

                    p.Add("@Id", deleteBo.Id, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPersonTableDel", p, commandType: CommandType.StoredProcedure);
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
        public ResponseBo<List<PersonTableListBo>> GetList(PersonTableGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonTableListBo>> responseBo = new ResponseBo<List<PersonTableListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@GroupId", criteriaBo.GroupId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@PersonTableStatId", criteriaBo.PersonTableStatId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonTableListBo>("spPersonTableList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonTableListBo>>();
            }

            return responseBo;
        }

        // Group
        public ResponseBo SaveGroup(PersonTableGroupBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@Id", saveBo.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@Name", saveBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@PersonTableGroupStatId", saveBo.PersonTableGroupStatId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@Order", saveBo.Order, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Notes", saveBo.Notes, DbType.String, ParameterDirection.Input, 255);

                    conn.Execute("spPersonTableGroupSave", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<PersonTableGroupBo> GetGroup(PersonTableGroupGetCriteriaBo criteriaBo)
        {
            ResponseBo<PersonTableGroupBo> responseBo = new ResponseBo<PersonTableGroupBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@Id", criteriaBo.Id, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonTableGroupBo>("spPersonTableGroupGet", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<PersonTableGroupBo>();
            }

            return responseBo;
        }

        public ResponseBo DeleteGroup(PersonTableGroupDeleteBo deleteBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, deleteBo);

                    p.Add("@Id", deleteBo.Id, DbType.Int64, ParameterDirection.Input);

                    conn.Execute("spPersonTableGroupDel", p, commandType: CommandType.StoredProcedure);
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

        public ResponseBo<List<PersonTableGroupListBo>> GetGroupList(PersonTableGroupGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<PersonTableGroupListBo>> responseBo = new ResponseBo<List<PersonTableGroupListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@PersonId", criteriaBo.PersonId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@PersonTableGroupStatId", criteriaBo.PersonTableGroupStatId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<PersonTableGroupListBo>("spPersonTableGroupList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<PersonTableGroupListBo>>();
            }

            return responseBo;
        }
    }
}