using Mh.Business.Bo.Person.Relation;
using Mh.Business.Bo.Person.Relation.Find;
using Mh.Business.Bo.Sys;
using System.Collections.Generic;

namespace Mh.Business.Contract.Person
{
    public interface IPersonRelationBusiness
    {
        ResponseBo<List<PersonRelationListBo>> GetList(PersonRelationGetListCriteriaBo criteriaBo);

        ResponseBo<List<PersonRelationFindListBo>> GetFindList(PersonRelationFindGetListCriteriaBo criteriaBo);

        ResponseBo<List<Enums.RelationTypes>> GetAvaibleTypeList(PersonRelationAvaibleTypeGetListCriteriaBo criteriaBo);

        ResponseBo Delete(PersonRelationDeleteBo deleteBo);

        ResponseBo Has(PersonRelationHasCriteriaBo criteriaBo);

        ResponseBo<List<long>> GetMyPersonIdList(long personId);
    }
}