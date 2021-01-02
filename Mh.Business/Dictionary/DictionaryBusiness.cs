using Dapper;
using Mh.Business.Bo.Dictionary;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Dictionary;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Mh.Business.Dictionary
{
    public class DictionaryBusiness : BaseBusiness, IDictionaryBusiness
    {
        public ResponseBo<List<DictionaryBo>> GetList()
        {
            ResponseBo<List<DictionaryBo>> responseBo = new ResponseBo<List<DictionaryBo>>();

            //try
            //{
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    // Do not get the product names. Their count can exceeds thousands.
                    responseBo.Bo = conn.Query<DictionaryBo>("select D.Id, D.[Key], D.IsForDesign, D.Tr, D.En from Dictionary D where D.IsProductName = 0").ToList();

                    responseBo.IsSuccess = true;
                }
            //}
            //catch (Exception ex)
            //{
            //    responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<DictionaryBo>>();
            //}

            return responseBo;
        }
    }
}