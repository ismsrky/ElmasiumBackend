using Mh.Business.Bo.Auth;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;

namespace Mh.Business.Contract.Auth
{
    public interface IAuthBusiness
    {
        ResponseBo<Mh.Sessions.SessionRealPerson> Login(LoginBo loginBo);

        ResponseBo Register(RegisterBo registerDto);

        ResponseBo Logout(Guid tokenId);

        void SaveSessionState(List<Sessions.Session> sessionList);

        ResponseBo VerifyEmail(Guid verifyEmailId);

        ResponseBo SendForgotPassword(AuthForgotPasswordBo forgotPasswordBo);

        ResponseBo SendVerifyEmail(BaseBo baseBo);
        ResponseBo IsEmailVerified(BaseBo baseBo);
    }
}