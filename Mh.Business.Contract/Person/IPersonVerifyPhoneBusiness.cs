using Mh.Business.Bo.Person.VerifyPhone;
using Mh.Business.Bo.Sys;

namespace Mh.Business.Contract.Person
{
    public interface IPersonVerifyPhoneBusiness
    {
        ResponseBo<PersonVerifyPhoneGenReturnBo> Gen(PersonVerifyPhoneGenBo genBo);
        ResponseBo Save(PersonVerifyPhoneSaveBo saveBo);
        ResponseBo IsVerified(PersonVerifyPhoneGenBo genBo);
    }
}