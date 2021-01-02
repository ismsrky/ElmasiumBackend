using Mh.Business.Bo.Person.Relation;
using Mh.Business.Bo.Person.Relation.Find;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Business.Person;
using Mh.Service.Dto.Person;
using Mh.Service.Dto.Person.Relation;
using Mh.Service.Dto.Person.Relation.Find;
using Mh.Service.Dto.Sys;
using Mh.Sessions;
using Mh.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonRelationController : BaseController
    {
        readonly IPersonRelationBusiness personRelationBusiness;
        public PersonRelationController(IPersonRelationBusiness _personRelationBusiness)
        {
            personRelationBusiness = _personRelationBusiness;
        }

        [HttpPost]
        public ResponseDto<List<PersonRelationListDto>> GetList(PersonRelationGetListCriteriaDto criteriaDto)
        {
            PersonRelationGetListCriteriaBo criteriaBo = new PersonRelationGetListCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,
                RelationTypeIdList = criteriaDto.RelationTypeIdList,
                IsOppositeOperation = criteriaDto.IsOppositeOperation,
                SearchRelationTypeOpp = criteriaDto.SearchRelationTypeOpp,
                CurrencyId = criteriaDto.CurrencyId,

                Name = criteriaDto.Name.IsNull() ? null : criteriaDto.Name.Trim(),

                PageOffSet = criteriaDto.PageOffSet,

                PersonRelationId = criteriaDto.PersonRelationId,

                BalanceStatIdList = criteriaDto.BalanceStatIdList,

                Session = Session
            };

            criteriaBo.Name = criteriaBo.Name.IsNull() ? null : criteriaBo.Name;
            if (criteriaBo.Name != null)
            {
                criteriaBo.Name = criteriaBo.Name.Trim();
            }

            ResponseBo<List<PersonRelationListBo>> responseBo = personRelationBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonRelationListDto>> responseDto = responseBo.ToResponseDto<List<PersonRelationListDto>, List<PersonRelationListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonRelationListDto>();

                responseDto.Dto = responseBo.Bo.GroupBy(x => x.RelatedPersonId).Select(x => x.First()).
                    Select(
                    x => new PersonRelationListDto()
                    {
                        Id = x.Id,
                        RelatedPersonId = x.RelatedPersonId,
                        RelatedPersonTypeId = x.RelatedPersonTypeId,
                        RelatedPersonFullName = x.RelatedPersonFullName,
                        RelatedPersonDefaultCurrencyId = x.RelatedPersonDefaultCurrencyId,
                        RelatedPersonUrlName = x.RelatedPersonUrlName,

                        IsMaster = x.IsMaster,
                        IsAlone = x.IsAlone,

                        Balance = x.Balance,
                        BalanceStatId = x.BalanceStatId,

                        //ApprovalStatId = x.ApprovalStatId,

                        //RelationTypeIdList = responseBo.Bo.Where(y => y.RelatedPersonId == x.RelatedPersonId).Select(y => y.RelationTypeId).ToList(),

                        RelationSubList = (from y in responseBo.Bo
                                           where y.RelatedPersonId == x.RelatedPersonId
                                           select new PersonRelationSubListDto
                                           {
                                               PersonRelationId = y.Id,
                                               ApprovalStatId = y.ApprovalStatId,
                                               RelationTypeId = y.RelationTypeId,
                                               ApprovalRelationId = y.ApprovalRelationId
                                           }).ToList()
                    }).ToList();


                // responseDto.Dto = Business.Stc.EnumShopTypeList.
                //GroupBy(x => x.GroupId).Select(x => x.First()).
                //Select(
                //x => new ShopTypeGroupDto()
                //{
                //    Id = x.GroupId,
                //    Name = x.GroupName,
                //    TypeList = Business.Stc.EnumShopTypeList.Where(y => y.GroupId == x.GroupId).Select(
                //        y => new ShopTypeDto()
                //        {
                //            Id = y.Id,
                //            Name = y.TypeName
                //        }).ToList()
                //}).ToList();

                //foreach (PersonRelationListBo itemBo in responseBo.Bo)
                //{

                //    responseDto.Dto.Add(new PersonRelationListDto()
                //    {
                //        Id = itemBo.Id,
                //        RelatedPersonId = itemBo.RelatedPersonId,
                //        RelatedPersonTypeId = itemBo.RelatedPersonTypeId,
                //        RelatedPersonFullName = itemBo.RelatedPersonFullName,
                //        IsMaster = itemBo.IsMaster,

                //        RelationTypeId = itemBo.RelationTypeId,
                //        RelationStateId = itemBo.RelationStateId
                //    });
                //}
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto<List<PersonRelationFindListDto>> GetFindList(PersonRelationFindGetListCriteriaDto criteriaDto)
        {
            if (criteriaDto.Name.IsNull() || criteriaDto.Name.Length < 3)
            {
                return new ResponseDto<List<PersonRelationFindListDto>>()
                {
                    IsSuccess = false,
                    Message = "Lütfen en az 3 karakter giriniz."
                };
            }

            PersonRelationFindGetListCriteriaBo criteriaBo = new PersonRelationFindGetListCriteriaBo()
            {
                ParentPersonId = criteriaDto.ParentPersonId,
                Name = criteriaDto.Name,
                RelationTypeId = criteriaDto.RelationTypeId,

                Session = Session
            };

            ResponseBo<List<PersonRelationFindListBo>> responseBo = personRelationBusiness.GetFindList(criteriaBo);

            ResponseDto<List<PersonRelationFindListDto>> responseDto = responseBo.ToResponseDto<List<PersonRelationFindListDto>, List<PersonRelationFindListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonRelationFindListDto>();
                foreach (PersonRelationFindListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonRelationFindListDto()
                    {
                        PersonId = itemBo.PersonId,
                        FullName = itemBo.FullName,
                        PersonTypeId = itemBo.PersonTypeId,

                        TaxNumber = itemBo.TaxNumber.IsNotNull() ? itemBo.TaxNumber.Substring(0, 3) + "****" : itemBo.TaxNumber,
                        TaxOffice = itemBo.TaxOffice,
                        Email = itemBo.Email,

                        ApprovalStatId = itemBo.ApprovalStatId,
                        PersonRelationId = itemBo.PersonRelationId,
                        ChildRelationTypeId = itemBo.ChildRelationTypeId,

                        IsParent = itemBo.IsParent
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<Enums.RelationTypes>> GetAvaibleTypeList(PersonRelationAvaibleTypeGetListCriteriaDto criteriaDto)
        {
            PersonRelationAvaibleTypeGetListCriteriaBo criteriaBo = new PersonRelationAvaibleTypeGetListCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,
                ChildPersonTypeId = criteriaDto.ChildPersonTypeId,

                OnlySearchables = criteriaDto.OnlySearchables,
                OnlyMasters = criteriaDto.OnlyMasters,

                Session = Session
            };

            ResponseBo<List<Enums.RelationTypes>> responseBo = personRelationBusiness.GetAvaibleTypeList(criteriaBo);

            ResponseDto<List<Enums.RelationTypes>> responseDto = responseBo.ToResponseDto<List<Enums.RelationTypes>, List<Enums.RelationTypes>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<Enums.RelationTypes>();
                foreach (Enums.RelationTypes itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(itemBo);
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Delete(PersonRelationDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonRelationDeleteBo deleteBo = new PersonRelationDeleteBo()
            {
                PersonRelationId = deleteDto.PersonRelationId,
                PersonId = deleteDto.PersonId,

                Session = Session
            };

            ResponseBo responseBo = personRelationBusiness.Delete(deleteBo);

            base.UpdateMyPersonIdList();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }

        [HttpPost]
        public ResponseDto Has(PersonRelationHasCriteriaDto criteriaDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonRelationHasCriteriaBo criteriaBo = new PersonRelationHasCriteriaBo()
            {
                RelationTypeId = criteriaDto.RelationTypeId,

                PersonId1 = criteriaDto.PersonId1,
                PersonId2 = criteriaDto.PersonId2,

                Session = Session
            };

            ResponseBo responseBo = personRelationBusiness.Has(criteriaBo);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }

        public static SessionMyPerson GetMyPerson(PersonChangeMyPersonDto myPersonDto, Session session)
        {
            // Getting my person //
            // Code below must return just one row.
            PersonRelationBusiness personRelationBusiness = new PersonRelationBusiness();
            PersonRelationGetListCriteriaBo criteriaBo = new PersonRelationGetListCriteriaBo();
            criteriaBo.PersonId = session.RealPerson.Id;
            criteriaBo.PersonRelationId = myPersonDto.PersonRelationId;
            criteriaBo.CurrencyId = myPersonDto.DefaultCurrencyId;
            criteriaBo.Session = session;
            PersonRelationListBo myPersonRelationBo = personRelationBusiness.GetList(criteriaBo).Bo[0];

            SessionMyPerson myPerson = new SessionMyPerson();
            myPerson.Id = myPersonDto.MyPersonId;
            myPerson.FullName = myPersonRelationBo.RelatedPersonFullName;
            myPerson.PersonTypeId = myPersonRelationBo.RelatedPersonTypeId;
            myPerson.RelationTypeId = myPersonRelationBo.RelationTypeId;
            myPerson.DefaultCurrencyId = myPersonRelationBo.RelatedPersonDefaultCurrencyId;
            myPerson.SelectedCurrencyId = myPerson.DefaultCurrencyId;

            return myPerson;
        }
    }
}