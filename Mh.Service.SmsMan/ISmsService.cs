using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.SmsMan
{
    public interface ISmsService
    {
        string UrlAddress { get; set; }
        string OtpUrlAddress { get; set; }

        string Username { get; set; }
        string Password { get; set; }

        string Caption { get; set; }
        string TextMessage { get; set; }

        string CompanyName { get; set; }
        bool IsSupportTr { get; set; }

        List<string> PhoneNumberList { get; set; }

        ServiceReturnBo Send();
        ServiceReturnBo SendOtp();
    }

    public class ServiceReturnBo
    {
        public ServiceReturnBo()
        {
            IsSuccess = false;
            ReturnValue = null;
        }

        public bool IsSuccess { get; set; }
        public string ReturnValue { get; set; } // null
    }
}