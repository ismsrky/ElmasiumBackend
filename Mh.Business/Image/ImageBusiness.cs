using Dapper;
using Mh.Business.Bo.Image;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Image;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Image
{
    public class ImageBusiness : BaseBusiness, IImageBusiness
    {
        public ResponseBo<List<ImageListCompressBo>> GetListCompress()
        {
            ResponseBo<List<ImageListCompressBo>> responseBo = new ResponseBo<List<ImageListCompressBo>>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    responseBo.Bo = conn.Query<ImageListCompressBo>("spImageListCompress", commandType: CommandType.StoredProcedure).ToList();

                    responseBo.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<List<ImageListCompressBo>>();
            }

            return responseBo;
        }

        public ResponseBo MarkAsCompressed(long id, bool isThumbnail)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@IsThumbnail", isThumbnail, DbType.Boolean, ParameterDirection.Input);

                    conn.Execute("spImageMarkCompressed", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo Save(ImageBo saveBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    base.AddStandartSpParams(ref p, saveBo);

                    p.Add("@ImageTypeId", saveBo.ImageTypeId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@ProductId", saveBo.ProductId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonId", saveBo.PersonId, DbType.Int64, ParameterDirection.Input);
                    p.Add("@PersonProductId", saveBo.PersonProductId, DbType.Int64, ParameterDirection.Input);

                    p.Add("@UniqueId", saveBo.UniqueId, DbType.Guid, ParameterDirection.Input);
                    p.Add("@FileTypeId", saveBo.FileTypeId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spImageSave", p, commandType: CommandType.StoredProcedure);
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