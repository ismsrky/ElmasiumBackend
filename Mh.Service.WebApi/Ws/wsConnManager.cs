using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Mh.Service.WebApi.Ws
{
    public class wsConnManager
    {
        public wsConnManager()
        {
            this.IsOk = false;
        }
        public string ProgramID { get; set; }
        public Guid TokenId { get; set; }

        public bool IsOk { get; set; }

        public static List<wsConnManager> ConnList { get; private set; }

        public static void InitAndStart()
        {
            try
            {
                if (Stc.ws != null)
                {
                    Stc.ws.Stop();
                    Stc.ws = null;
                }
            }
            catch (Exception ex)
            {
            }

            ConnList = new List<wsConnManager>();

            Stc.ws = new WebSocketServer(Stc.wsPort, Stc.IsSecure);
            if(Stc.IsSecure)
            {
                Stc.ws.SslConfiguration.ServerCertificate = new X509Certificate2("C:\\inetpub\\vhosts\\elmasium.com\\certs\\elmasium_cert_2019.pfx", "353419");
                Stc.ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Default | SslProtocols.Ssl2 | (SslProtocols)(SslProtocols.Tls12 | SslProtocols.Tls11);
            }

            Stc.ws.AddWebSocketService<Ws.MhMainPath>(Stc.wsPath);

            Stc.ws.KeepClean = false;
            Stc.ws.ReuseAddress = false;
            Stc.ws.AllowForwardedRequest = true;

            Stc.ws.Start();
        }


        public static wsConnManager Start(string programId)
        {
            wsConnManager wsConn = new wsConnManager();
            wsConn.ProgramID = programId;

            try
            {
                //Api.ApiConnController apiConnController = new Api.ApiConnController();
                //ApiConnStartDto apiConnInsertDto = new ApiConnStartDto();
                //apiConnInsertDto.ProgramId = programId;
                //apiConnInsertDto.StartTime = DateTime.Now;
                //apiConnInsertDto.DeviceTypeId = Enums.DeviceTypes.Pc;

                //Response response = apiConnController.Start(apiConnInsertDto);
                //wsConn.ConnDbId = response.ReturnedId.Value;
            }
            catch (Exception ex)
            {
            }

            ConnList.Add(wsConn);

            return wsConn;
        }
        public static void Close(string programId, string reason, bool wasClean, ushort code)
        {
            wsConnManager wsConn = Get(programId);

            ConnList.RemoveAll(x => x.ProgramID == programId);

            try
            {
                //ApiConnCloseDto apiConnCloseDto = new ApiConnCloseDto();
                //apiConnCloseDto.Id = wsConn.ConnDbId;
                //apiConnCloseDto.ApiConnCloseReasonId = (Enums.ApiConnCloseReasons)code;
                //apiConnCloseDto.CloseReason = reason;
                //apiConnCloseDto.CloseWasClean = wasClean;

                //Api.ApiConnController controller = new Api.ApiConnController();
                //controller.Close(apiConnCloseDto);
            }
            catch (Exception ex)
            {
            }

            //SessionManager.SessionList.RemoveAll(x => x.ProgramID == ID);
        }

        public static wsConnManager Get(string programId)
        {
            if (ConnList == null || ConnList.Count == 0) return null;

            wsConnManager t_conn = ConnList.Where(x => x.ProgramID == programId).FirstOrDefault();
            return t_conn;
        }

        public static List<wsConnManager> GetByTokenId(Guid tokenId)
        {
            if (ConnList == null || ConnList.Count == 0) return null;

            List<wsConnManager> t_conn = ConnList.Where(x => x.TokenId == tokenId).ToList();
            return t_conn;
        }

        public void CloseConnection(CloseStatusCode statusCode, string reason)
        {
            try
            {
                Stc.ws.WebSocketServices[Stc.wsPath].Sessions.CloseSession(ProgramID, statusCode, reason);
            }
            catch (Exception ex)
            {
            }
        }

        public void Send(string data)
        {
            try
            {
                Stc.ws.WebSocketServices[Stc.wsPath].Sessions.SendTo(data, ProgramID);
            }
            catch (Exception ex)
            {
            }
        }
    }
}