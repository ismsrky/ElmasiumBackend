using Mh.Utils;
using System;

namespace Mh.Service.WebApi.Ws
{
    public class CommandProcess
    {
        public string Process(string fullCommand, string programId)
        {
            try
            {
                Sessions.Session session = Stc.SessionManager.Get(fullCommand.ToGuid());
                Ws.wsConnManager.Get(programId).TokenId = fullCommand.ToGuid();
                if (session != null)
                {
                    return "ok";
                }
            }
            catch (Exception ex)
            {
                return "fail";
            }
            return "fail";
        }
        public void ProcessAsync(string fulllCommand, string sessionProgramId, Action<string> completed)
        {
            Func<string, string, string> sender = Process;
            sender.BeginInvoke(
              fulllCommand, sessionProgramId,
              ar =>
              {
                  try
                  {
                      var sent = sender.EndInvoke(ar);
                      if (completed != null)
                          completed(sent);
                  }
                  catch (Exception ex)
                  {
                      //      _logger.Error(ex.ToString());
                      //      error(
                      //  "An error has occurred during the callback for an async send.",
                      //  ex
                      //);
                  }
              },
              null
            );
        }
    }
}