using Dapper;
using Mh.Business.Bo.Auth;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Auth;
using Mh.Sessions;
using Mh.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Mh.Business.Auth
{
    public class AuthBusiness : BaseBusiness, IAuthBusiness
    {
        public ResponseBo<SessionRealPerson> Login(LoginBo loginBo)
        {
            string locationInfo = base.GetLocationFromIp(loginBo.ClientIpAddress);
            ResponseBo<SessionRealPerson> responseBo = new ResponseBo<SessionRealPerson>();

            try
            {
                #region Checks             
                if (loginBo.Username.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xEmailCantBeEmpty", loginBo.LanguageId);

                    return responseBo;
                }
                if (loginBo.Password.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xPasswordCantBeEmpty", loginBo.LanguageId);

                    return responseBo;
                }
                #endregion

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@Username", loginBo.Username, DbType.String, ParameterDirection.Input, 250);
                    p.Add("@Password", loginBo.Password, DbType.String, ParameterDirection.Input, 250);
                    p.Add("@LoginTime", loginBo.LoginTime, DbType.DateTime, ParameterDirection.Input);
                    p.Add("@TokenId", loginBo.TokenId, DbType.Guid, ParameterDirection.Input);
                    p.Add("@LanguageIdInput", loginBo.LanguageId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@ClientIpAddress", loginBo.ClientIpAddress, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@LocationInfo", locationInfo, DbType.String, ParameterDirection.Input, 4000);

                    p.Add("@AnonymousApiSessionId", loginBo.AnonymousApiSessionId, DbType.Int64, ParameterDirection.Input);

                    responseBo.Bo = conn.Query<SessionRealPerson>("spAuthLogin", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    //var user = conn.Execute("spAuthLogin", p, commandType: CommandType.StoredProcedure);
                    //responseBo.Message = p.Get<string>("@Message");
                    //responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    //if (responseBo.IsSuccess)
                    //{
                    //    apiSessionId = p.Get<long?>("ApiSessionId");

                    //    responseBo.Bo = new Sessions.SessionRealPerson()
                    //    {
                    //        Id = p.Get<long>("@PersonId"),
                    //        Name = p.Get<string>("@Name"),
                    //        Surname = p.Get<string>("@Surname"),
                    //        LanguageId = languageId,
                    //        DefaultCurrencyId = p.Get<Enums.Currencies>("@DefaultCurrencyId"),
                    //        PersonRelationId = p.Get<long>("@PersonRelationId")
                    //        //GenderId = p.Get<Enums.Genders>("@GenderId")
                    //    };
                    //}
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null).ToResponse<Mh.Sessions.SessionRealPerson>();
            }

            return responseBo;
        }

        public ResponseBo Register(RegisterBo registerBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                Enums.Languages langId = registerBo.LanguageId;

                #region Checks
                if (registerBo == null)
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xInvalidData", langId);

                    return responseBo;
                }
                if (registerBo.Username.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xEmailCantBeEmpty", langId);

                    return responseBo;
                }
                if (registerBo.Name.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xFirstNameCantBeEmpty", langId);

                    return responseBo;
                }
                if (registerBo.Surname.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xLastNameCantBeEmpty", langId);

                    return responseBo;
                }

                if (registerBo.Password.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xPasswordCantBeEmpty", langId);

                    return responseBo;
                }
                if (Validation.ValidateEmail(registerBo.Username) == false)
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xEnterValidMailAddress", langId);

                    return responseBo;
                }

                if (registerBo.HaveShopToo && registerBo.ShopShortName.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xShopSignNameCantBeEmpty", langId);

                    return responseBo;
                }
                if (registerBo.HaveShopToo && registerBo.ShopTypeId.IsNull())
                {
                    responseBo.IsSuccess = false;
                    responseBo.Message = GetDicValue("xSelectShopType", langId);

                    return responseBo;
                }
                #endregion

                // we may change it later.
                // but username equals to email.
                registerBo.Email = registerBo.Username;

                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", dbType: DbType.Int64, direction: ParameterDirection.Output);
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@Name", registerBo.Name, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@Surname ", registerBo.Surname, DbType.String, ParameterDirection.Input, 50);
                    p.Add("@Username", registerBo.Username, DbType.String, ParameterDirection.Input, 250);
                    p.Add("@Password", registerBo.Password, DbType.String, ParameterDirection.Input, 250);
                    p.Add("@Email", registerBo.Email, DbType.String, ParameterDirection.Input, 250);
                    p.Add("@LanguageId", registerBo.LanguageId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@GenderId", registerBo.GenderId, DbType.Int32, ParameterDirection.Input);
                    p.Add("@Birthdate", registerBo.Birthdate, DbType.Date, ParameterDirection.Input);

                    p.Add("@HaveShopToo", registerBo.HaveShopToo, DbType.Boolean, ParameterDirection.Input);
                    p.Add("@ShopShortName", registerBo.ShopShortName, DbType.String, ParameterDirection.Input, 255);
                    p.Add("@ShopTypeId", registerBo.ShopTypeId, DbType.Int32, ParameterDirection.Input);

                    var user = conn.Execute("spAuthRegister", p, commandType: CommandType.StoredProcedure);
                    responseBo.ReturnedId = p.Get<long?>("@Id");
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo Logout(Guid tokenId)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    p.Add("@TokenId ", tokenId, DbType.Guid, ParameterDirection.Input);

                    var user = conn.Execute("spAuthLogout", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public void SaveSessionState(List<Sessions.Session> sessionList)
        {
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    conn.Execute("delete from ApiSessionState", commandType: CommandType.Text);
                }

                if (sessionList == null || sessionList.Count == 0) return;


                foreach (Session session in sessionList.Where(x => x.LogoutTime == null))
                {
                    using (SqlConnection conn = DbAccess.Connection.GetConn())
                    {
                        var p = new DynamicParameters();
                        p.Add("@TokenId", session.TokenId, DbType.Guid, ParameterDirection.Input);
                        p.Add("@ApiSessionId", session.ApiSessionId, DbType.Int64, ParameterDirection.Input);
                        p.Add("@ClientIpAddress ", session.ClientIpAddress, DbType.String, ParameterDirection.Input, 50);

                        p.Add("@LoginTime", session.LoginTime, DbType.DateTime, ParameterDirection.Input);
                        p.Add("@LogoutTime", session.LogoutTime, DbType.DateTime, ParameterDirection.Input);

                        p.Add("@RealPerson ", JsonConvert.SerializeObject(session.RealPerson), DbType.String, ParameterDirection.Input, 4000);
                        p.Add("@MyPerson ", JsonConvert.SerializeObject(session.MyPerson), DbType.String, ParameterDirection.Input, 4000);

                        conn.Execute("spApiSessionStateSave", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
            catch (Exception ex)
            {
                base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }
        }

        public List<Session> GetSessionList()
        {
            List<Session> sessionList = null;
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    sessionList = conn.Query<Session>(@"
                    select
                    ApiSessionId,
                    TokenId,
                    LoginTime,
                    LogoutTime,
                    ClientIpAddress,
                    RealPerson RealPersonJson,
                    MyPerson MyPersonJson 
                    from ApiSessionState", commandType: CommandType.Text).ToList();
                }

                if (sessionList != null && sessionList.Count > 0)
                {
                    foreach (Session session in sessionList)
                    {
                        session.RealPerson = JsonConvert.DeserializeObject<SessionRealPerson>(session.RealPersonJson);
                        session.MyPerson = JsonConvert.DeserializeObject<SessionMyPerson>(session.MyPersonJson);
                    }
                }
            }
            catch (Exception ex)
            {
                base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return sessionList;
        }

        public ResponseBo VerifyEmail(Guid verifyEmailId)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@EmailVerifyId ", verifyEmailId, DbType.Guid, ParameterDirection.Input);

                    conn.Execute("spAuthVerifyEmail", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo SendForgotPassword(AuthForgotPasswordBo forgotPasswordBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@Email ", forgotPasswordBo.Email, DbType.String, ParameterDirection.Input,255);
                    p.Add("@LanguageId ", forgotPasswordBo.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spNotificationEMailPrepareForgotPassword", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, null);
            }

            return responseBo;
        }

        public ResponseBo SendVerifyEmail(BaseBo baseBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId ", baseBo.Session.RealPerson.Id, DbType.String, ParameterDirection.Input, 255);

                    conn.Execute("spNotificationEMailPrepareVerification", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo);
            }

            return responseBo;
        }

        public ResponseBo IsEmailVerified(BaseBo baseBo)
        {
            ResponseBo responseBo = new ResponseBo();

            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@PersonId ", baseBo.Session.RealPerson.Id, DbType.String, ParameterDirection.Input, 255);

                    conn.Execute("spAuthIsEmailVerified", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, baseBo);
            }

            return responseBo;
        }
    }
}