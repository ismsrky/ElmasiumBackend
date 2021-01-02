using Mh.Business.Bo.Person;
using Mh.Business.Bo.Sys;
using Mh.Business.Person;
using Mh.Service.Dto.Person;
using Mh.Service.Dto.Sys;
using Mh.Service.WebApi.Ws;
using Mh.Sessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mh.Service.WebApi
{
    public class BaseController : ApiController
    {
        //public Mh.Sessions.Session Session { get; private set; }

        protected Sessions.SessionManager SessionManager
        {
            get
            {
                return Stc.SessionManager;
            }
        }

        protected Sessions.Session Session
        {
            get
            {
                Guid? tokenId = Request.GetTokenId();
                if (tokenId == null) return null;

                return SessionManager.Get(tokenId.Value);
            }
        }

        public BaseController()//(Sessions.Session session)
        {
            //Session = session;
        }


        protected void SendNotifyWsToList(List<PersonNotifyListBo> notifyPersonListBo)
        {
            if (notifyPersonListBo == null || notifyPersonListBo.Count == 0) return;

            List<PersonNotifyListDto> toSend = null;
            PersonNotifyListBo tempBo = null;
            string serializedJson = null;
            foreach (Session session in SessionManager.SessionList)
            {
                toSend = new List<PersonNotifyListDto>();
                foreach (long myPersonId in session.RealPerson.MyPersonIdList)
                {
                    tempBo = notifyPersonListBo.FirstOrDefault(x => x.PersonId == myPersonId);
                    if (tempBo == null) continue;

                    toSend.Add(new PersonNotifyListDto()
                    {
                        PersonId = myPersonId,
                        WsNotificationGroupTypeId = tempBo.WsNotificationGroupTypeId
                    });
                }

                // now let's send this info to all connection that session has.
                if (toSend.Count > 0)
                {
                    serializedJson = JsonConvert.SerializeObject(toSend);

                    List<wsConnManager> wsConnList = wsConnManager.GetByTokenId(session.TokenId);
                    if (wsConnList == null || wsConnList.Count == 0) continue;
                    foreach (wsConnManager wsConn in wsConnList)
                    {
                        if (wsConn.IsOk == false) continue;

                        wsConn.Send(serializedJson);
                    }
                }
            }
        }

        /// <summary>
        /// This method updates 'MyPersonIdList' of every person in session.
        /// This kills performance but I did not have much time to make this perfect.
        /// Later, change this method that it just updates the given person.
        /// </summary>
        protected void UpdateMyPersonIdList()
        {
            PersonRelationBusiness personRelationBusiness = new PersonRelationBusiness();

            foreach (Session session in SessionManager.SessionList)
            {
                try
                {
                    session.RealPerson.MyPersonIdList = personRelationBusiness.GetMyPersonIdList(session.RealPerson.Id).Bo;
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        ///  This method is old and not used right now. Delete later.
        /// </summary>
        /// <param name="wsNotifyType"></param>
        /// <param name="personIdList"></param>
        public void SendNotifyWsToList(Enums.WsNotificationTypes wsNotifyType, List<long> personIdList)
        {
            //I send every client to check the count. I dont want to send computed count because of performance issues.
            //If i send computed count to everyone, maybe some of them already disconnected, so it would cause performance reduction.
            if (personIdList == null || personIdList.Count == 0) return;

            foreach (Sessions.Session session in SessionManager.SessionList)
            {
                try
                {
                    if (personIdList.Contains(session.RealPerson.Id) == false) continue;
                    List<wsConnManager> wsConnList = wsConnManager.GetByTokenId(session.TokenId);
                    if (wsConnList == null || wsConnList.Count == 0) continue;

                    foreach (wsConnManager wsConn in wsConnList)
                    {
                        if (wsConn.IsOk == false) continue;

                        wsConn.Send(((int)wsNotifyType).ToString());
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected BaseBo ToBaseBo()
        {
            BaseBo baseBo = new BaseBo();

            baseBo.Session = Session.Copy();

            return baseBo;
        }
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}

        protected ResponseDto SaveExLog(Exception exception, Type type, string methodName)
        {
            Business.BaseBusiness baseBusiness = new Business.BaseBusiness();
            BaseBo baseBo = new BaseBo();
            baseBo.Session = Session;

            return baseBusiness.SaveExLog(exception, type, methodName, baseBo, Enums.ApplicationTypes.MhServiceWebApi).ToResponseDto();
        }

        protected string GetImageName(Guid? imageUniqueId, Enums.FileTypes? imageFileTypeId)
        {
            if (imageUniqueId == null || imageFileTypeId == null) return null;

            return imageUniqueId.Value.ToString().ToUpper() + "." + imageFileTypeId.Value.ToString().ToLower();
        }
    }
}