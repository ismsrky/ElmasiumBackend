using Mh.Business.Bo.Sys;
using Mh.Business.Bo.Warning;
using Mh.Business.Contract.Warning;
using Mh.Service.Dto.Sys;
using Mh.Service.Dto.Warning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mh.Service.WebApi.Warning
{
    public class WarningController : BaseController
    {
        readonly IWarningBusiness warningBusiness;
        public WarningController(IWarningBusiness _warningBusiness)
        {
            warningBusiness = _warningBusiness;
        }

        [HttpPost]
        public ResponseDto Save(WarningDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            WarningBo saveBo = new WarningBo()
            {
                Id = saveDto.Id,
                WarningModuleTypeId = saveDto.WarningModuleTypeId,

                PersonProductId = saveDto.PersonProductId,
                CommentId = saveDto.CommentId,
                PersonId = saveDto.PersonId,

                WarningTypeId = saveDto.WarningTypeId,
                Notes = saveDto.Notes,

                Session = Session
            };

            ResponseBo responseBo = warningBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SaveCheck(WarningCheckDto saveCheckDto)
        {
            ResponseDto responseDto = new ResponseDto();

            WarningCheckBo saveCheckBo = new WarningCheckBo()
            {
                WarningModuleTypeId = saveCheckDto.WarningModuleTypeId,

                PersonProductId = saveCheckDto.PersonProductId,
                CommentId = saveCheckDto.CommentId,
                PersonId = saveCheckDto.PersonId,

                Session = Session
            };

            ResponseBo responseBo = warningBusiness.SaveCheck(saveCheckBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}