using Mh.Business.Bo.Person;
using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Person.Relation;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Service.Dto.Person.Relation;
using Mh.Service.Dto.Sys;
using Mh.Sessions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http;


namespace Mh.Service.WebApi.Person
{
    public class PersonRelationRuleController : BaseController
    {
        readonly IPersonRelationRuleBusiness personRelationRuleBusiness;

        public PersonRelationRuleController(IPersonRelationRuleBusiness _personRelationRuleBusiness)
        {
            personRelationRuleBusiness = _personRelationRuleBusiness;
        }

        [HttpPost]
        
        public ResponseDto<List<PersonRelationRuleListDto>> GetList(PersonRelationRuleGetListCriteriaDto criteriaDto)
        {
            PersonRelationRuleGetListCriteriaBo criteriaBo = new PersonRelationRuleGetListCriteriaBo()
            {
                PersonId = criteriaDto.PersonId,

                Session = Session
            };


            ResponseBo<List<PersonRelationRuleListBo>> responseBo = personRelationRuleBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonRelationRuleListDto>> responseDto = responseBo.ToResponseDto<List<PersonRelationRuleListDto>, List<PersonRelationRuleListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonRelationRuleListDto>();
                foreach (PersonRelationRuleListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonRelationRuleListDto()
                    {
                        Id = itemBo.Id,
                        ParentPersonTypeId = itemBo.ParentPersonTypeId,
                        ChildPersonTypeId = itemBo.ChildPersonTypeId,
                        ParentRelationTypeId = itemBo.ParentRelationTypeId,
                        ChildRelationTypeId = itemBo.ChildRelationTypeId
                    });
                }
            }

            return responseDto;
        }
    }
}