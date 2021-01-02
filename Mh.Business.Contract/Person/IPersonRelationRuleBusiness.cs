using Mh.Business.Bo.Person.Relation;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonRelationRuleBusiness
    {
        ResponseBo<List<PersonRelationRuleListBo>> GetList(PersonRelationRuleGetListCriteriaBo criteriaBo);
    }
}