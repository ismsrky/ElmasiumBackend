using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using Mh.Service.Dto.Fiche;
using Mh.Service.Dto.Fiche.Product;
using Mh.Service.Dto.Sys;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Fiche
{
    public class FicheProductController:BaseController
    {
        readonly IFicheProductBusiness ficheProductBusiness;
        public FicheProductController(IFicheProductBusiness _ficheProductBusiness)
        {
            ficheProductBusiness = _ficheProductBusiness;
        }


        [HttpPost]
        public ResponseDto<List<FicheProductListDto>> GetList(FicheProductGetListCriteriaDto criteriaDto)
        {
            FicheProductGetListCriteriaBo criteriaBo = new FicheProductGetListCriteriaBo()
            {
                FicheId = criteriaDto.FicheId,

                Session = Session
            };

            ResponseBo<List<FicheProductListBo>> responseBo = ficheProductBusiness.GetList(criteriaBo);

            ResponseDto<List<FicheProductListDto>> responseDto = responseBo.ToResponseDto<List<FicheProductListDto>, List<FicheProductListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<FicheProductListDto>();
                foreach (FicheProductListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new FicheProductListDto()
                    {
                        Id = itemBo.Id,

                        ProductId = itemBo.ProductId,
                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,

                        Quantity = itemBo.Quantity,
                        UnitPrice = itemBo.UnitPrice,
                        Total = itemBo.Total,
                        DiscountRate = itemBo.DiscountRate,
                        DiscountTotal = itemBo.DiscountTotal,
                        VatRate = itemBo.VatRate,
                        VatTotal = itemBo.VatTotal,
                        GrandTotal = itemBo.GrandTotal,

                        Notes = itemBo.Notes,

                        IsDeleted = itemBo.IsDeleted
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateProducts(FicheDto ficheDto)
        {
            ResponseDto responseDto = new ResponseDto();

            if (ficheDto==null|| ficheDto.ProductList == null || ficheDto.ProductList.Count == 0)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = Business.Stc.GetDicValue("xNoProduct", Session.RealPerson.LanguageId);
                return responseDto;
            }

            FicheBo ficheBo = new FicheBo()
            {
                DebtPersonId = ficheDto.DebtPersonId,
                CreditPersonId = ficheDto.CreditPersonId,
                CurrencyId = ficheDto.CurrencyId,

                // We do not need other fields.

                Session = Session
            };
            ficheBo.ProductList = (from x in ficheDto.ProductList
                                       //where !x.IsDeleted
                                   select new FicheProductBo
                                   {
                                       Id = x.Id,
                                       ProductId = x.ProductId,
                                       Quantity = x.Quantity,
                                       UnitPrice = x.UnitPrice,
                                       DiscountRate = x.DiscountRate,
                                       DiscountTotal = x.DiscountTotal,
                                       VatRate = x.VatRate,

                                       Notes = x.Notes,
                                       IsDeleted = x.IsDeleted
                                   }).ToList();

            ResponseBo responseBo = ficheProductBusiness.UpdateProducts(ficheBo);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }
    }
}