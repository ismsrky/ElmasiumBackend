using Mh.Business.Bo.Product.Code;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using Mh.Utils;

namespace Mh.Service.WebApi.Product
{
    public class ProductCodeController : BaseController
    {
        readonly IProductCodeBusiness productCodeBusiness;

        public ProductCodeController(IProductCodeBusiness _productCodeBusiness)
        {
            productCodeBusiness = _productCodeBusiness;
        }

        [HttpPost]
        public ResponseDto<List<ProductCodeListDto>> GetList(ProductCodeGetListCriteriaDto criteriaDto)
        {
            ProductCodeGetListCriteriaBo criteriaBo = new ProductCodeGetListCriteriaBo()
            {
                ProductId = criteriaDto.ProductId,

                Session = Session
            };

            ResponseBo<List<ProductCodeListBo>> responseBo = productCodeBusiness.GetList(criteriaBo);

            ResponseDto<List<ProductCodeListDto>> responseDto = responseBo.ToResponseDto<List<ProductCodeListDto>, List<ProductCodeListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ProductCodeListDto>();
                foreach (ProductCodeListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ProductCodeListDto()
                    {
                        Id = itemBo.Id,

                        Code = itemBo.Code,
                        ProductCodeTypeId = itemBo.ProductCodeTypeId,

                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime(),
                        UpdateDateNumber = itemBo.UpdateDate.ToNumberFromDateTimeNull()
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Save(ProductCodeDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductCodeBo saveBo = new ProductCodeBo()
            {
                Code = saveDto.Code,
                ProductCodeTypeId = saveDto.ProductCodeTypeId,

                ProductId = saveDto.ProductId,

                Session = Session
            };

            ResponseBo responseBo = productCodeBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}