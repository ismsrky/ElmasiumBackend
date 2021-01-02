using Dapper;
using Mh.Business.Bo.Notification.Preference;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Notification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Notification
{
    public class NotificationPreferenceBusiness : BaseBusiness, INotificationPreferenceBusiness
    {
        public ResponseBo<List<NotificationPreferenceListBo>> GetList(BaseBo baseBo)
        {
            ResponseBo<List<NotificationPreferenceListBo>> responseBo = new ResponseBo<List<NotificationPreferenceListBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, baseBo);

                    responseBo.Bo = conn.Query<NotificationPreferenceListBo>("spNotificationPreferenceList", p, commandType: CommandType.StoredProcedure).ToList();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo).ToResponse<List<NotificationPreferenceListBo>>();
            }

            return responseBo;
        }

        public ResponseBo Save(NotificationPreferenceSaveBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@PreferenceListJson", JsonConvert.SerializeObject(saveBo.PreferenceList), DbType.String, ParameterDirection.Input, int.MaxValue);

                    conn.Execute("spNotificationPreferenceSave", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
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