using Mh.Business.Bo.Person.Account;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Account;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonAccountController: BaseController
    {
        readonly IPersonAccountBusiness personAccountBusiness;
        public PersonAccountController(IPersonAccountBusiness _personAccountBusiness)
        {
            personAccountBusiness = _personAccountBusiness;
        }

        [HttpPost]
        public ResponseDto<PersonAccountDto> Get(PersonAccountGetCriteriaDto criteriaDto)
        {
            PersonAccountGetCriteriaBo criteriaBo = new PersonAccountGetCriteriaBo()
            {
                AccountId = criteriaDto.AccountId,

                Session = Session
            };

            ResponseBo<PersonAccountBo> responseBo = personAccountBusiness.Get(criteriaBo);

            ResponseDto<PersonAccountDto> responseDto = responseBo.ToResponseDto<PersonAccountDto, PersonAccountBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonAccountDto()
                {
                    Id = responseBo.Bo.Id,
                    Name = responseBo.Bo.Name,
                    AccountTypeId = responseBo.Bo.AccountTypeId,
                    CurrencyId = responseBo.Bo.CurrencyId,
                    StatId = responseBo.Bo.StatId,
                    IsDefault = responseBo.Bo.IsDefault,
                    Balance = responseBo.Bo.Balance,
                    Notes = responseBo.Bo.Notes,

                    IsFastRetail = responseBo.Bo.IsFastRetail
                };

                /*
               if (responseBo.Bo.PersonId != Session.RealPerson.Id) // warning hacking
                 {
                     responseDto.IsSuccess = false;
                     responseDto.Message = Business.Stc.GetDicValue("xHackingAttemptDetected", Session.RealPerson.LanguageId);
                     responseDto.Dto = null;
                 }
                 else
                 {
                     responseDto.Dto = new PersonAccountDto()
                     {
                         Id = responseBo.Bo.Id,
                         PersonId = responseBo.Bo.PersonId,
                         Name = responseBo.Bo.Name,
                         AccountTypeId = responseBo.Bo.AccountTypeId,
                         StatId = responseBo.Bo.StatId,
                         Balance = responseBo.Bo.Balance,
                         Notes = responseBo.Bo.Notes
                     };
                 }   
              */
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonAccountListDto>> GetList(PersonAccountGetListCriteriaDto criteriaDto)
        {
            PersonAccountGetListCriteriaBo criteriaBo = new PersonAccountGetListCriteriaBo()
            {
                OwnerPersonId = criteriaDto.OwnerPersonId,

                AccountTypeIdList = criteriaDto.AccountTypeIdList,
                CurrencyId = criteriaDto.CurrencyId,
                StatId = criteriaDto.StatId,

                Session = Session
            };

            ResponseBo<List<PersonAccountListBo>> responseBo = personAccountBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonAccountListDto>> responseDto = responseBo.ToResponseDto<List<PersonAccountListDto>, List<PersonAccountListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonAccountListDto>();
                foreach (PersonAccountListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonAccountListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        AccountTypeId = itemBo.AccountTypeId,
                        CurrencyId = itemBo.CurrencyId,
                        StatId = itemBo.StatId,
                        Balance = itemBo.Balance,

                        IsFastRetail = itemBo.IsFastRetail
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(PersonAccountDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonAccountBo saveBo = new PersonAccountBo()
            {
                Id = saveDto.Id,
                Name = saveDto.Name,
                AccountTypeId = saveDto.AccountTypeId,
                CurrencyId = saveDto.CurrencyId,
                StatId = saveDto.StatId,
                IsDefault = saveDto.IsDefault,
                Notes = saveDto.Notes,

                IsFastRetail = saveDto.IsFastRetail,

                Session = Session
            };

            ResponseBo responseBo = personAccountBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Delete(PersonAccountDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonAccountDeleteBo deleteBo = new PersonAccountDeleteBo()
            {
                AccountId = deleteDto.AccountId,

                Session = Session
            };

            ResponseBo responseBo = personAccountBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonAccountActivityListDto>> GetActivityList(PersonAccountActivityGetListCriteriaDto criteriaDto)
        {
            PersonAccountActivityGetListCriteriaBo criteriaBo = new PersonAccountActivityGetListCriteriaBo()
            {
                OwnerPersonId = criteriaDto.OwnerPersonId,
                AccountIdList = criteriaDto.AccountIdList,

                ApprovalStatIdList = criteriaDto.ApprovalStatIdList,

                GrandTotalMin = criteriaDto.GrandTotalMin,
                GrandTotalMax = criteriaDto.GrandTotalMax,

                IssueDateStart = criteriaDto.IssueDateStartNumber.ToDateTimeFromNumberNull(),
                IssueDateEnd = criteriaDto.IssueDateEndNumber.ToDateTimeFromNumberNull(),

                CurrencyId = criteriaDto.CurrencyId,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<PersonAccountActivityListBo>> responseBo = personAccountBusiness.GetActivityList(criteriaBo);

            ResponseDto<List<PersonAccountActivityListDto>> responseDto = responseBo.ToResponseDto<List<PersonAccountActivityListDto>, List<PersonAccountActivityListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonAccountActivityListDto>();
                foreach (PersonAccountActivityListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonAccountActivityListDto()
                    {
                        Id = itemBo.Id,
                        Total = itemBo.Total,

                        AccountId = itemBo.AccountId,
                        AccountName = itemBo.AccountName,
                        AccountTypeId = itemBo.AccountTypeId,
                        IsDebt = itemBo.IsDebt,

                        OwnerPersonId = itemBo.OwnerPersonId,

                        FicheId = itemBo.FicheId,
                        FicheCurrencyId = itemBo.FicheCurrencyId,
                        FicheApprovalStatId = itemBo.FicheApprovalStatId,
                        FicheIssueDateNumber = itemBo.FicheIssueDate.ToNumberFromDateTime()
                    });
                }
            }

            return responseDto;
        }


        /// <summary>
        /// I wrote this method seperated from 'Get' because of performance concerns.
        /// This method will be one of our most used methods at a time.
        /// </summary>
        /// <param name="criteriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseDto GetFastRetail(PersonAccountGetFastRetailCriteriaDto criteriaDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonAccountGetFastRetailCriteriaBo criteriaBo = new PersonAccountGetFastRetailCriteriaBo()
            {
                AccountTypeId = criteriaDto.AccountTypeId,
                CurrencyId = criteriaDto.CurrencyId,

                Session = Session
            };

            ResponseBo responseBo = personAccountBusiness.GetFastRetail(criteriaBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}