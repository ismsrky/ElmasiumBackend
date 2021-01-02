using Dapper;
using Mh.Business.Bo.EnumsOp;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.EnumsOp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.EnumsOp
{
    public class EnumsOpBusiness : BaseBusiness, IEnumsOpBusiness
    {
        public ResponseBo<List<CurrenciesBo>> GetCurrencyList()
        {
            ResponseBo<List<CurrenciesBo>> responseBo = new ResponseBo<List<CurrenciesBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<CurrenciesBo>(@"
                    select C.Id,C.[Name],C.Code,C.IconClass from EnumCurrencies C", commandType: CommandType.Text).ToList();

                    responseBo.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<CurrenciesBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<ShopTypeBo>> GetShopTypeList()
        {
            ResponseBo<List<ShopTypeBo>> responseBo = new ResponseBo<List<ShopTypeBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<ShopTypeBo>(@"
                    select
                    ST.Id,ST.[Name] TypeName,ST.GroupId,STG.[Name] GroupName, STG.[Order] GroupOrder
                    from EnumShopType ST
                    inner join EnumShopTypeGroup STG on ST.GroupId = STG.Id", commandType: CommandType.Text).ToList();
                    responseBo.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<ShopTypeBo>>();
            }

            return responseBo;
        }

        public ResponseBo<List<FicheContentBo>> GetFicheContentList()
        {
            ResponseBo<List<FicheContentBo>> responseBo = new ResponseBo<List<FicheContentBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<FicheContentBo>(@"
                    select
                    FC.Id,FC.[Name] TypeName,FC.GroupId,FCG.[Name] GroupName
                    from EnumFicheContent FC
                    inner join EnumFicheContentGroup FCG on FC.GroupId = FCG.Id", commandType: CommandType.Text).ToList();
                    responseBo.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<FicheContentBo>>();
            }

            return responseBo;
        }
    }
}