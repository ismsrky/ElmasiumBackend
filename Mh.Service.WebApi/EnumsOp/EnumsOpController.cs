using Mh.Service.Dto.EnumsOp;
using Mh.Service.Dto.Sys;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.EnumsOp
{
    public class EnumsOpController : BaseController
    {
        [HttpPost]
        public ResponseDto<List<ShopTypeGroupDto>> GetShopTypeList()
        {
            ResponseDto<List<ShopTypeGroupDto>> responseDto = new ResponseDto<List<ShopTypeGroupDto>>();
            responseDto.IsSuccess = true;

            Enums.Languages languageId = Session.RealPerson.LanguageId;

            responseDto.Dto = Business.Stc.EnumShopTypeList.
                GroupBy(x => x.GroupId).Select(x => x.First()).
                Select(
                x => new ShopTypeGroupDto()
                {
                    Id = x.GroupId,
                    Name = Business.Stc.GetDicValue(x.GroupName, languageId),
                    GroupOrder = x.GroupOrder,
                    TypeList = Business.Stc.EnumShopTypeList.Where(y => y.GroupId == x.GroupId).Select(
                        y => new ShopTypeDto()
                        {
                            Id = y.Id,
                            Name = Business.Stc.GetDicValue(y.TypeName, languageId)
                        }).ToList()
                }).OrderBy(k => k.GroupOrder).ToList();

            return responseDto;
        }

        [HttpPost]
        [AllowAnonymous]
        public ResponseDto<List<FicheContentGroupDto>> GetFicheContentList()
        {
            ResponseDto<List<FicheContentGroupDto>> responseDto = new ResponseDto<List<FicheContentGroupDto>>();
            responseDto.IsSuccess = true;

            responseDto.Dto = Business.Stc.EnumFicheContentList.
                GroupBy(x => x.GroupId).Select(x => x.First()).
                Select(
                x => new FicheContentGroupDto()
                {
                    Id = x.GroupId,
                    Name = x.GroupName,
                    ContentList = Business.Stc.EnumFicheContentList.Where(y => y.GroupId == x.GroupId).Select(
                        y => new FicheContentDto()
                        {
                            Id = y.Id,
                            Name = y.TypeName
                        }).ToList()
                }).ToList();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<CurrenciesDto>> GetCurrencyList()
        {
            ResponseDto<List<CurrenciesDto>> responseDto = new ResponseDto<List<CurrenciesDto>>();
            responseDto.IsSuccess = true;

            responseDto.Dto = (from x in Business.Stc.EnumCurrencyList
                               select new CurrenciesDto()
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   Code = x.Code,
                                   IconClass = x.IconClass
                               }).ToList();

            return responseDto;
        }
    }
}