using Mh.Business.Bo.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using Mh.Service.Dto.Product;
using Mh.Service.Dto.Sys;
using Mh.Service.WebApi.BarkodokuComService;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Web.Http;

namespace Mh.Service.WebApi.Product
{
    public class ProductController : BaseController
    {
        readonly IProductBusiness productBusiness;

        public ProductController(IProductBusiness _productBusiness)
        {
            productBusiness = _productBusiness;
        }

        [HttpPost]
        public ResponseDto Save(ProductSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductSaveBo saveBo = new ProductSaveBo()
            {
                Id = saveDto.Id,
                Name = saveDto.Name,
                ProductTypeId = saveDto.ProductTypeId,

                PersonId = saveDto.PersonId,

                SalePrice = saveDto.SalePrice,
                PurhasePrice = saveDto.PurhasePrice,
                CurrencyId = saveDto.CurrencyId,
                VatRate = saveDto.VatRate,

                Barcode = saveDto.Barcode,

                Session = Session
            };

            ResponseBo responseBo = productBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SaveStar(ProductStarDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductStarBo saveBo = new ProductStarBo()
            {
                ProductId = saveDto.ProductId,
                Star = saveDto.Star,

                Session = Session
            };

            ResponseBo responseBo = productBusiness.SaveStar(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Update(ProductUpdateDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductUpdateBo updateBo = new ProductUpdateBo()
            {
                ProductId = updateDto.ProductId,
                ProductUpdateTypeId = updateDto.ProductUpdateTypeId,

                Name = updateDto.Name,
                OriginCountryId = updateDto.OriginCountryId,
                Notes = updateDto.Notes,
                CategoryId = updateDto.CategoryId,

                ImageUniqueId = updateDto.ImageUniqueId,
                ImageFileTypeId = updateDto.ImageFileTypeId,

                Session = Session
            };

            ResponseBo responseBo = productBusiness.Update(updateBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto UpdateCheck(ProductUpdateCheckDto updateCheckDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductUpdateCheckBo updateCheckBo = new ProductUpdateCheckBo()
            {
                ProductId = updateCheckDto.ProductId,
                ProductUpdateTypeId = updateCheckDto.ProductUpdateTypeId,

                Session = Session
            };

            ResponseBo responseBo = productBusiness.UpdateCheck(updateCheckBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto GetNextId()
        {
            return productBusiness.GetNextId().ToResponseDto();
        }

        /// <summary>
        /// This method looks from external sources.
        /// Please run this only a barcode not found in db.
        /// This method doest not add to person product table but only product table.
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        internal ResponseDto AddFromExternalSource(string barcode, Sessions.Session session, long? shopId)
        {
            ResponseDto responseDto = new ResponseDto();
            try
            {
                BarkodServisSoapClient client = new BarkodServisSoapClient();
                BarkodSonuc barkodSonuc = client.BarkodGetir(Stc.BarkodOkuComKey, barcode);

                if (barkodSonuc.HataMesaji != null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = barkodSonuc.HataMesaji.HataAciklama;

                    return responseDto;
                }

                ProductSaveBo saveBo = new ProductSaveBo();
                saveBo.Session = session;

                saveBo.Name = barkodSonuc.UrunBarkod.UrunAd;
                saveBo.ProductTypeId = Enums.ProductTypes.xShopping;

                saveBo.PersonId = shopId;

                saveBo.VatRate = 18;

                saveBo.Barcode = barcode;

                if (barkodSonuc.UrunBarkod.UrunFiyat != null && barkodSonuc.UrunBarkod.UrunFiyat.Length > 0)
                {
                    saveBo.SalePrice = Convert.ToDecimal(barkodSonuc.UrunBarkod.UrunFiyat[0].UrunFiyat);
                    saveBo.PurhasePrice = saveBo.SalePrice * (decimal)0.8;
                }
                else
                {
                    saveBo.SalePrice = 10;
                    saveBo.PurhasePrice = 8;
                }

                saveBo.CurrencyId = Enums.Currencies.xTurkishLira;

                saveBo.RefSourceJson = JsonConvert.SerializeObject(barkodSonuc);

                responseDto = productBusiness.Save(saveBo).ToResponseDto();
            }
            catch (Exception ex)
            {
                responseDto = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name);
            }
            return responseDto;
        }
    }
}