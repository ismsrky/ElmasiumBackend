using Mh.Business.Bo.Auth;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Auth;
using Mh.Business.Contract.Person;
using Mh.Business.Person;
using Mh.Service.Dto.Auth;
using Mh.Service.Dto.Dictionary;
using Mh.Service.Dto.Person;
using Mh.Service.Dto.Sys;
using Mh.Service.WebApi.Person;
using Mh.Sessions;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Linq;
using Mh.Service.WebApi.Attribute;

namespace Mh.Service.WebApi.Auth
{
    public class AuthController : BaseController
    {
        readonly IAuthBusiness authBusiness;
        readonly IRealPersonBusiness realPersonBusiness;

        public AuthController(IAuthBusiness _authBusiness, IRealPersonBusiness _realPersonBusiness)
        {
            authBusiness = _authBusiness;
            realPersonBusiness = _realPersonBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        public ResponseDto<LoginReturnDto> Login(LoginDto loginDto)
        {
            DateTime loginTime = DateTime.Now;
            Guid tokenId = Guid.NewGuid();

            string clientIpAddress = HttpContext.Current.Request.UserHostAddress;

            long? anonymousApiSessionId = null;

            if (Session != null && Session.RealPerson.Id == -2)
            {
                anonymousApiSessionId = Session.ApiSessionId;
            }

            LoginBo loginBo = new LoginBo()
            {
                Username = loginDto.Username,
                Password = loginDto.Password,
                LanguageId = loginDto.LanguageId,
                LoginTime = loginTime,
                TokenId = tokenId,
                ClientIpAddress = clientIpAddress,
                AnonymousApiSessionId = anonymousApiSessionId
            };

            ResponseBo<SessionRealPerson> response = authBusiness.Login(loginBo);

            ResponseDto<LoginReturnDto> responseReturn = response.ToResponseDto<LoginReturnDto, SessionRealPerson>();

            if (response.IsSuccess)
            {
                // We need to login first, then we will get my person data.
                Sessions.Session session = SessionManager.Login(response.Bo, loginTime, tokenId, clientIpAddress);

                PersonRelationBusiness personRelationBusiness = new PersonRelationBusiness();
                session.RealPerson.MyPersonIdList = personRelationBusiness.GetMyPersonIdList(session.RealPerson.Id).Bo;

                PersonChangeMyPersonDto personChangeMyPersonDto = new PersonChangeMyPersonDto();
                personChangeMyPersonDto.MyPersonId = response.Bo.Id;
                personChangeMyPersonDto.PersonRelationId = response.Bo.PersonRelationId;
                personChangeMyPersonDto.DefaultCurrencyId = response.Bo.DefaultCurrencyId;
                SessionMyPerson myPerson = PersonRelationController.GetMyPerson(personChangeMyPersonDto, session);

                session.MyPerson = myPerson;

                ////if user decides to change language in login screen, then we change it
                //if (loginDto.LanguageId != session.RealPerson.LanguageId)
                //{
                //    BaseBo baseBo = base.ToBaseBo();
                //    baseBo.Session.RealPerson.LanguageId = loginDto.LanguageId;
                //    realPersonBusiness.ChangeLanguage(baseBo);
                //}

                responseReturn.Dto = new LoginReturnDto()
                {
                    Id = response.Bo.Id,
                    TokenId = session.TokenId,
                    Name = response.Bo.Name,
                    Surname = response.Bo.Surname,
                    GenderId = response.Bo.GenderId,
                    DefaultCurrencyId = response.Bo.DefaultCurrencyId
                };
            }
            else
            {
                responseReturn.Dto = null;
            }

            // System.Threading.Thread.Sleep(5000);
            return responseReturn;
        }

        [AllowAnonymous]
        [HttpPost]
        public ResponseDto<LoginReturnDto> LoginAsAnonymous(LoginAsAnonymousDto loginAsAnonymousDto)
        {
            if (loginAsAnonymousDto.IsNull() || loginAsAnonymousDto.K958Q8C4JZ4EX8E != "V5QGCXW49GXA5GMXANJ2RYHA58N3B8NBPDCYJQW67B22ZNJ5WG")
            {
                return new ResponseDto<LoginReturnDto>()
                {
                    IsSuccess = false,
                    Message = Business.Stc.GetDicValue("xUnauthorizedOperation", Enums.Languages.xEnglish)
                };
            }

            LoginDto loginDto = new LoginDto();
            loginDto.Username = "anonymous@elmasium.com";
            loginDto.Password = "FFLqZVG=2){*Rwjs[?_64x_?{%!ar8zEwjV4;5PG-+Es&Tf.-t";
            loginDto.LanguageId = loginAsAnonymousDto.LanguageId;
            ResponseDto<LoginReturnDto> responseDto = Login(loginDto);

            if (responseDto.Dto != null)
            {
                responseDto.Dto.Name = Business.Stc.GetDicValue(responseDto.Dto.Name, loginAsAnonymousDto.LanguageId);
                responseDto.Dto.Surname = Business.Stc.GetDicValue(responseDto.Dto.Surname, loginAsAnonymousDto.LanguageId);
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Logout()
        {
            ResponseBo responseBo = authBusiness.Logout(Session.TokenId);

            SessionManager.Logout(Session.TokenId);

            return responseBo.ToResponseDto();
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseDto IsLogin()
        {
            Sessions.Session session = null;
            LoginAttribute loginAttribute = new LoginAttribute();
            bool result = loginAttribute.CheckAuthorized(ActionContext, out session);

            return new ResponseDto() { IsSuccess = result };
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseDto IsRealLogin()
        {
            if (Session == null)
            {
                return new ResponseDto()
                {
                    IsSuccess = false
                };
            }

            return new ResponseDto()
            {
                IsSuccess = Session.RealPerson.Id != -2
            };
        }

        [HttpPost]
        public ResponseDto IsAdmin()
        {
            bool isAdmin = Stc.AdminRealIdList.Contains(Session.RealPerson.Id);

            return new ResponseDto()
            {
                IsSuccess = isAdmin
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseDto Register(RegisterDto registerDto)
        {
            //if (registerDto.Password != registerDto.RePassword)
            //{
            //    return new ResponseDto()
            //    {
            //        IsSuccess = false,
            //        Message = Business.Stc.GetDicValue("xPasswordNotMatch", registerDto.LanguageId)
            //    };
            //}


            RegisterBo registerBo = new RegisterBo
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Password = registerDto.Password,
                Username = registerDto.Username,
                LanguageId = registerDto.LanguageId,
                GenderId = registerDto.GenderId,
                Birthdate = registerDto.BirthdateNumber.ToDateTimeFromNumberNull(),

                HaveShopToo = registerDto.HaveShopToo,
                ShopShortName = registerDto.ShopShortName,
                ShopTypeId = registerDto.ShopTypeId
            };

            return authBusiness.Register(registerBo).ToResponseDto();
        }

        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage VerifyEmail(string id)
        {
            string error = null;
            string result = "";

            if (id.IsGuid())
            {
                Guid g_id = id.ToGuid();
                ResponseBo responseBo = authBusiness.VerifyEmail(g_id);

                if (responseBo.IsSuccess)
                {
                    result = "<!DOCTYPE html><html><head> <meta charset='utf-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <title>Elmasium</title></head><body> <meta http-equiv='refresh' content='5; url=https://www.elmasium.com/'/><h3 style='position: absolute; top: 50%; left: 50%; -moz-transform: translateX(-50%) translateY(-50%); -webkit-transform: translateX(-50%) translateY(-50%); transform: translateX(-50%) translateY(-50%);'><center><p>E-postanızı doğruladığınız için teşekkür ederiz.</p><p>5 saniye içinde otomatik olarak yönlendirileceksiniz.</p><a href='https://elmasium.com/'>Elmasium.com</a><img src='https://elmasium.com/assets/common/elmaium_verify_email_icon.png' alt='ok'></center></h3></body></html>";
                }
                else
                {
                    error = responseBo.Message;
                }
            }
            else
            {
                error = "Geçersiz değer";
            }
            
            if (error.IsNotNull())
            {
                result = "<!DOCTYPE html><html><head> <meta charset='utf-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <title>Elmasium</title></head><body><h3 style='position: absolute; top: 50%; left: 50%; -moz-transform: translateX(-50%) translateY(-50%); -webkit-transform: translateX(-50%) translateY(-50%); transform: translateX(-50%) translateY(-50%);'><center><p>{{@error}}</p><a href='https://elmasium.com/'>Elmasium.com</a><img src='https://elmasium.com/assets/common/elmasium_error_icon.png' alt='ok'></center></h3></body></html>";
                result = result.Replace("{{@error}}", error);
            }

            var response = new HttpResponseMessage();
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;

           // return deger;
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseDto SendForgotPassword(AuthForgotPasswordDto forgotPasswordDto)
        {
            AuthForgotPasswordBo forgotPasswordBo = new AuthForgotPasswordBo
            {
                Email = forgotPasswordDto.Email,
                LanguageId = forgotPasswordDto.LanguageId,
            };

            return authBusiness.SendForgotPassword(forgotPasswordBo).ToResponseDto();
        }

        [HttpPost]
        public ResponseDto SendVerifyEmail()
        {
            ResponseBo responseBo = authBusiness.SendVerifyEmail(base.ToBaseBo());

            return responseBo.ToResponseDto();
        }

        [HttpPost]
        public ResponseDto IsEmailVerified()
        {
            ResponseBo responseBo = authBusiness.IsEmailVerified(base.ToBaseBo());

            return responseBo.ToResponseDto();
        }
    }
}