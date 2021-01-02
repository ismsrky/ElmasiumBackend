using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Relation;
using Mh.Business.Bo.Person.Real;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using Mh.Service.Dto.Fiche.Relation;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Web.Http;


namespace Mh.Service.WebApi.Fiche
{
    public class FicheRelationController : BaseController
    {
        readonly IFicheRelationBusiness ficheRelationBusiness;
        public FicheRelationController(IFicheRelationBusiness _ficheRelationBusiness)
        {
            ficheRelationBusiness = _ficheRelationBusiness;
        }        
    }
}