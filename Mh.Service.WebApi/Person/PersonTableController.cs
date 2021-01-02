using Mh.Business.Bo.Person.Table;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Table;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonTableController : BaseController
    {
        readonly IPersonTableBusiness personTableBusiness;

        public PersonTableController(IPersonTableBusiness _personTableBusiness)
        {
            personTableBusiness = _personTableBusiness;
        }

        [HttpPost]
        public ResponseDto Save(PersonTableDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonTableBo saveBo = new PersonTableBo()
            {
                Id = saveDto.Id,

                GroupId = saveDto.GroupId,

                Name = saveDto.Name,
                PersonTableStatId = saveDto.PersonTableStatId,
                Order = saveDto.Order,
                Notes = saveDto.Notes,

                Session = Session
            };

            ResponseBo responseBo = personTableBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
        [HttpPost]
        public ResponseDto<PersonTableDto> Get(PersonTableGetCriteriaDto criteriaDto)
        {
            PersonTableGetCriteriaBo criteriaBo = new PersonTableGetCriteriaBo()
            {
                Id = criteriaDto.Id,

                Session = Session
            };

            ResponseBo<PersonTableBo> responseBo = personTableBusiness.Get(criteriaBo);

            ResponseDto<PersonTableDto> responseDto = responseBo.ToResponseDto<PersonTableDto, PersonTableBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonTableDto()
                {
                    Id = responseBo.Bo.Id,

                    GroupId = responseBo.Bo.GroupId,

                    Name = responseBo.Bo.Name,
                    PersonTableStatId = responseBo.Bo.PersonTableStatId,
                    Order = responseBo.Bo.Order,
                    Notes = responseBo.Bo.Notes
                };
            }

            return responseDto;
        }
        [HttpPost]
        public ResponseDto Delete(PersonTableDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonTableDeleteBo deleteBo = new PersonTableDeleteBo()
            {
                Id = deleteDto.Id,

                Session = Session
            };

            ResponseBo responseBo = personTableBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
        [HttpPost]
        public ResponseDto<List<PersonTableListDto>> GetList(PersonTableGetListCriteriaDto criteriaDto)
        {
            PersonTableGetListCriteriaBo criteriaBo = new PersonTableGetListCriteriaBo()
            {
                GroupId = criteriaDto.GroupId,
                PersonTableStatId = criteriaDto.PersonTableStatId,

                Session = Session
            };

            ResponseBo<List<PersonTableListBo>> responseBo = personTableBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonTableListDto>> responseDto = responseBo.ToResponseDto<List<PersonTableListDto>, List<PersonTableListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonTableListDto>();
                foreach (PersonTableListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonTableListDto()
                    {
                        Id = itemBo.Id,

                        Name = itemBo.Name,
                        PersonTableStatId = itemBo.PersonTableStatId,
                        Order = itemBo.Order,

                        LastTableFicheId = itemBo.LastTableFicheId,
                        TableFicheStatId = itemBo.TableFicheStatId,
                        FicheCurrencyId = itemBo.FicheCurrencyId,
                        FicheDebtPersonId = itemBo.FicheDebtPersonId,
                        FicheDebtPersonFullName = itemBo.FicheDebtPersonFullName,
                        FicheGrandTotal = itemBo.FicheGrandTotal,
                        FicheCreateDateNumber = itemBo.FicheCreateDate.ToNumberFromDateTimeNull()
                    });
                }
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto SaveGroup(PersonTableGroupDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonTableGroupBo saveBo = new PersonTableGroupBo()
            {
                Id = saveDto.Id,
                PersonId = saveDto.PersonId,

                Name = saveDto.Name,
                PersonTableGroupStatId = saveDto.PersonTableGroupStatId,
                Order = saveDto.Order,
                Notes = saveDto.Notes,

                Session = Session
            };

            ResponseBo responseBo = personTableBusiness.SaveGroup(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<PersonTableGroupDto> GetGroup(PersonTableGroupGetCriteriaDto criteriaDto)
        {
            PersonTableGroupGetCriteriaBo criteriaBo = new PersonTableGroupGetCriteriaBo()
            {
                Id = criteriaDto.Id,

                Session = Session
            };

            ResponseBo<PersonTableGroupBo> responseBo = personTableBusiness.GetGroup(criteriaBo);

            ResponseDto<PersonTableGroupDto> responseDto = responseBo.ToResponseDto<PersonTableGroupDto, PersonTableGroupBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonTableGroupDto()
                {
                    Id = responseBo.Bo.Id,
                    PersonId = responseBo.Bo.PersonId,

                    Name = responseBo.Bo.Name,
                    PersonTableGroupStatId = responseBo.Bo.PersonTableGroupStatId,
                    Order = responseBo.Bo.Order,
                    Notes = responseBo.Bo.Notes
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto DeleteGroup(PersonTableGroupDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonTableGroupDeleteBo deleteBo = new PersonTableGroupDeleteBo()
            {
                Id = deleteDto.Id,

                Session = Session
            };

            ResponseBo responseBo = personTableBusiness.DeleteGroup(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonTableGroupListDto>> GetGroupList(PersonTableGroupGetListCriteriaDto criteriaDto)
        {
            PersonTableGroupGetListCriteriaBo criteriaBo = new PersonTableGroupGetListCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,
                PersonTableGroupStatId = criteriaDto.PersonTableGroupStatId,

                Session = Session
            };

            ResponseBo<List<PersonTableGroupListBo>> responseBo = personTableBusiness.GetGroupList(criteriaBo);

            ResponseDto<List<PersonTableGroupListDto>> responseDto = responseBo.ToResponseDto<List<PersonTableGroupListDto>, List<PersonTableGroupListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonTableGroupListDto>();
                foreach (PersonTableGroupListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonTableGroupListDto()
                    {
                        Id = itemBo.Id,

                        Name = itemBo.Name,
                        PersonTableGroupStatId = itemBo.PersonTableGroupStatId,

                        Order = itemBo.Order,

                        TableCountsList = itemBo.TableCountsList == null ? null :
                        (from o in itemBo.TableCountsList
                         select new PersonTableCountsDto()
                         {
                             PersonTableStatId = o.PersonTableStatId,
                             Count = o.Count
                         }).ToList()
                    });
                }
            }

            return responseDto;
        }
    }
}