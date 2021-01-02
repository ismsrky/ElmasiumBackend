using Mh.Business.Bo.Sys;

namespace Mh.Business.Bo.Person.VerifyPhone
{
    public class PersonVerifyPhoneSaveBo : BaseBo
    {
        public long Id { get; set; }
        public string VerifyCode { get; set; }
    }
}