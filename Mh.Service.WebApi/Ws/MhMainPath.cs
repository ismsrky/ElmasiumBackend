using WebSocketSharp;
using WebSocketSharp.Server;

namespace Mh.Service.WebApi.Ws
{
    public class MhMainPath : WebSocketBehavior
    {
        void Bitti(bool deger)
        {
        }

        //void geldi(string gonderilecel)
        //{
        //    SendAsync(gonderilecel, Bitti);
        //}
        protected override void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                CommandProcess cmd = new CommandProcess();
                string result = cmd.Process(e.Data, ID);
                if (result == "ok")
                {
                    wsConnManager.Get(ID).IsOk = true;
                }
                SendAsync(result, Bitti);
            }
            catch
            {
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            wsConnManager.Close(ID, e.Reason, e.WasClean, e.Code);

            base.OnClose(e);
        }

        protected override void OnOpen()
        {
            wsConnManager.Start(ID);

            base.OnOpen();
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }
}