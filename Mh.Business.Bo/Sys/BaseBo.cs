using Mh.Sessions;

namespace Mh.Business.Bo.Sys
{
    public class BaseBo
    {
        //public long OperatorRealId { get; set; } // The real person who performs this action.
        //public Enums.Languages LanguageId { get; set; }

        //public string ClientIpAddress { get; set; }
        //public long? ApiSessionId { get; set; }

        public Mh.Sessions.Session Session { get; set; }
    }
}