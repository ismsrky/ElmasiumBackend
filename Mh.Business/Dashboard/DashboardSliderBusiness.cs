using Dapper;
using Mh.Business.Bo.Dashboard.Slider;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Dashboard;
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

namespace Mh.Business.Dashboard
{
    public class DashboardSliderBusiness : BaseBusiness, IDashboardSliderBusiness
    {
        public ResponseBo<List<DashboardSliderGroupListBo>> GetGroupList(BaseBo baseBo)
        {
            ResponseBo<List<DashboardSliderGroupListBo>> responseBo = new ResponseBo<List<DashboardSliderGroupListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, baseBo);

                    responseBo.Bo = conn.Query<DashboardSliderGroupListBo>("spDashboardSliderGroupList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<List<DashboardSliderGroupListBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<DashboardSliderListBo>> GetList(DashboardSliderGetListCriteriaBo criteriaBo)
        {
            ResponseBo<List<DashboardSliderListBo>> responseBo = new ResponseBo<List<DashboardSliderListBo>>();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, criteriaBo);

                    p.Add("@GroupId", criteriaBo.GroupId, DbType.Int32, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<DashboardSliderListBo>("spDashboardSliderList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<List<DashboardSliderListBo>>();
            }

            return responseBo;
        }
    }
}