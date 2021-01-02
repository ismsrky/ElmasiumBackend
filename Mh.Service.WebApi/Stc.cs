using Mh.Business.Bo.Sys;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using WebSocketSharp.Server;

namespace Mh.Service.WebApi
{
    public static class Stc
    {
        public static Mh.Sessions.SessionManager SessionManager { get; set; }

        public static void ReadConfigs()
        {
            var appSettings = ConfigurationManager.AppSettings;

            ConnStr = appSettings.Get("ConnStr");
            IsSecure = appSettings.Get("IsSecure") == "1" ? true : false;
            BarkodOkuComKey = appSettings.Get("BarkodOkuComKey");
            ImageSourceUrl = appSettings.Get("ImageSourceUrl");
        }

        public static string ConnStr { get; private set; }
        public static bool IsSecure { get; set; }
        public static string BarkodOkuComKey { get; set; }
        public static string ImageSourceUrl { get; set; }

        public const int wsPort = 1618;
        public const string wsPath = "/MhMainPath";
        public static WebSocketServer ws;

        public readonly static List<long> AdminRealIdList = new List<long>() { 1, 11858 };

        public static Guid? GetTokenId(this HttpRequestMessage request)
        {
            Guid tokenId;

            try
            {
                if (request == null
                || request.Headers.Authorization == null
                || request.Headers.Authorization.Scheme.IsNull())
                {
                    return null;
                }

                string strTokenId = request.Headers.Authorization.Scheme;

                if (Guid.TryParse(strTokenId, out tokenId) == false)//tokenId guid olmalıdır.
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

            return tokenId;
        }

        public static ResponseDto ToResponseDto(this ResponseBo responseBo)
        {
            if (responseBo == null) return null;

            ResponseDto responseDto = new ResponseDto
            {
                IsSuccess = responseBo.IsSuccess,
                Message = responseBo.Message,
                HasException = responseBo.HasException,
                ReturnedId = responseBo.ReturnedId
            };

            return responseDto;
        }
        public static ResponseDto<dto> ToResponseDto<dto, bo>(this ResponseBo<bo> responseBo)
        {
            if (responseBo == null) return null;

            ResponseDto<dto> responseDto = new ResponseDto<dto>
            {
                IsSuccess = responseBo.IsSuccess,
                Message = responseBo.Message,
                HasException = responseBo.HasException,
                ReturnedId = responseBo.ReturnedId
            };

            return responseDto;
        }

        public static ResponseDto<dto> ToResponseDto<dto>(this ResponseBo responseBo)
        {
            if (responseBo == null) return null;

            ResponseDto<dto> responseDto = new ResponseDto<dto>
            {
                IsSuccess = responseBo.IsSuccess,
                Message = responseBo.Message,
                HasException = responseBo.HasException,
                ReturnedId = responseBo.ReturnedId
            };

            return responseDto;
        }
    }
}