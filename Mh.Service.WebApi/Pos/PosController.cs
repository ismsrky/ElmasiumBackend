using Mh.Business.Bo.Pos;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Pos;
using Mh.Service.Dto.Pos;
using Mh.Service.Dto.Sys;
using Mh.Sessions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace Mh.Service.WebApi.Pos
{
    public class PosController : BaseController
    {
        readonly IPosBusiness posBusiness;
        public PosController(IPosBusiness _posBusiness)
        {
            posBusiness = _posBusiness;
        }

        [HttpPost]
        
        public ResponseDto<List<PosProductShortCutGroupListDto>> GetShortCutList([FromBody]JObject paramss)
        {
            var osoos = paramss.GetValue("shopId");
            long shopId = osoos.Value<long>();

            ResponseBo<List<PosProductShortCutListBo>> responseBo = posBusiness.GetShortCutList(shopId, Session.RealPerson.Id, Session.RealPerson.LanguageId);

            ResponseDto<List<PosProductShortCutGroupListDto>> responseDto = responseBo.ToResponseDto<List<PosProductShortCutGroupListDto>, List<PosProductShortCutListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PosProductShortCutGroupListDto>();

                responseDto.Dto = responseBo.Bo.
                    GroupBy(x => x.GroupId).Select(x => x.First()).
                    Select(
                    x => new PosProductShortCutGroupListDto()
                    {
                        Id = x.GroupId,
                        Name = x.GroupName,
                        ProductList = responseBo.Bo.Where(y => y.GroupId == x.GroupId && y.ProductId > 0).Select(
                               y => new PosProductShortCutListDto()
                               {
                                   Id = y.Id,
                                   ProductId = y.ProductId,
                                   ProductName = y.ProductName
                               }).ToList()
                    }).ToList();
            }

            return responseDto;
        }

        [HttpPost]
        
        public ResponseDto SaveShortCut(PosProductShortCutDto posProductShortCutDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PosProductShortCutBo posProductShortCutBo = new PosProductShortCutBo()
            {
                ShopId = posProductShortCutDto.ShopId,
                ProductId = posProductShortCutDto.ProductId,
                GroupId = posProductShortCutDto.GroupId,

                Session = Session
            };

            ResponseBo responseBo = posBusiness.SaveShortCut(posProductShortCutBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        
        public ResponseDto DeleteShortCut(PosProductShortCutDto posProductShortCutDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PosProductShortCutBo posProductShortCutBo = new PosProductShortCutBo()
            {
                Id = posProductShortCutDto.Id,
                ShopId = posProductShortCutDto.ShopId,
                ProductId = posProductShortCutDto.ProductId,
                GroupId = posProductShortCutDto.GroupId,

                Session = Session
            };

            ResponseBo responseBo = posBusiness.DeleteShortCut(posProductShortCutBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        
        public ResponseDto SaveShortCutGroup(PosProductShortCutGroupDto posProductShortCutGroupDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PosProductShortCutGroupBo posProductShortCutGroupBo = new PosProductShortCutGroupBo()
            {
                Id = posProductShortCutGroupDto.Id,
                Name = posProductShortCutGroupDto.Name,
                ShopId = posProductShortCutGroupDto.ShopId,

                Session = Session
            };

            ResponseBo responseBo = posBusiness.SaveShortCutGroup(posProductShortCutGroupBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        
        public ResponseDto DeleteShortCutGroup(PosProductShortCutGroupDto posProductShortCutGroupDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PosProductShortCutGroupBo posProductShortCutGroupBo = new PosProductShortCutGroupBo()
            {
                Id = posProductShortCutGroupDto.Id,
                Name = posProductShortCutGroupDto.Name,
                ShopId = posProductShortCutGroupDto.ShopId,

                Session = Session
            };

            ResponseBo responseBo = posBusiness.DeleteShortCutGroup(posProductShortCutGroupBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}