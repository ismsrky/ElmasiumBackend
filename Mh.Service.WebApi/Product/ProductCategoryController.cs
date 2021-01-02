using Mh.Business.Bo.Product.Category;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using Mh.Service.Dto.Product.Category;
using Mh.Service.Dto.Sys;
using Mh.Service.WebApi.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mh.Service.WebApi.Product
{
    public class ProductCategoryController : BaseController
    {
        readonly IProductCategoryBusiness productCategoryBusiness;
        public ProductCategoryController(IProductCategoryBusiness _productCategoryBusiness)
        {
            productCategoryBusiness = _productCategoryBusiness;
        }

        [HttpPost]
        public ResponseDto<List<ProductCategoryListDto>> GetList(ProductCategoryGetListCriteriaDto criteriaDto)
        {
            ProductCategoryGetListCriteriaBo criteriaBo = new ProductCategoryGetListCriteriaBo()
            {
                ProductCategoryId = criteriaDto.ProductCategoryId,
                IsUpper = criteriaDto.IsUpper,

                Session = Session
            };

            ResponseBo<List<ProductCategoryListBo>> responseBo = productCategoryBusiness.GetList(criteriaBo);

            ResponseDto<List<ProductCategoryListDto>> responseDto = responseBo.ToResponseDto<List<ProductCategoryListDto>, List<ProductCategoryListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ProductCategoryListDto>();
                foreach (ProductCategoryListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ProductCategoryListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,

                        UrlName = itemBo.UrlName,

                        IsLast = itemBo.IsLast,

                        ParentId = itemBo.ParentId
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<ProductCategoryRawListDto>> GetRawList(ProductCategoryGetListAdminCriteriaDto criteriaDto)
        {
            ProductCategoryGetListAdminCriteriaBo criteriaBo = new ProductCategoryGetListAdminCriteriaBo()
            {
                ParentId = criteriaDto.ParentId,

                Session = Session
            };

            ResponseBo<List<ProductCategoryListAdminBo>> responseBo = productCategoryBusiness.GetListAdmin(criteriaBo);

            ResponseDto<List<ProductCategoryRawListDto>> responseDto = responseBo.ToResponseDto<List<ProductCategoryRawListDto>, List<ProductCategoryListAdminBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ProductCategoryRawListDto>();
                foreach (ProductCategoryListAdminBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ProductCategoryRawListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,

                        ParentId = itemBo.ParentId
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        [Admin]
        public ResponseDto Save(ProductCategoryRawListDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductCategoryListBo saveBo = new ProductCategoryListBo()
            {
                Id = saveDto.Id,
                Name = saveDto.Name,
                ParentId = saveDto.ParentId,

                Session = Session
            };

            ResponseBo responseBo = productCategoryBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        [Admin]
        public ResponseDto Delete(ProductCategoryRawListDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            ProductCategoryListBo deleteBo = new ProductCategoryListBo()
            {
                Id = deleteDto.Id,

                Session = Session
            };

            ResponseBo responseBo = productCategoryBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ProductCategoryCheckUrlDto> CheckUrl(ProductCategoryCheckUrlCriteriaDto criteriaDto)
        {
            ProductCategoryCheckUrlCriteriaBo criteriaBo = new ProductCategoryCheckUrlCriteriaBo()
            {
                UrlSegmentList = criteriaDto.UrlSegmentList,

                Session = Session
            };

            ResponseBo<ProductCategoryCheckUrlBo> responseBo = productCategoryBusiness.CheckUrl(criteriaBo);

            ResponseDto<ProductCategoryCheckUrlDto> responseDto = responseBo.ToResponseDto<ProductCategoryCheckUrlDto, ProductCategoryCheckUrlBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ProductCategoryCheckUrlDto()
                {
                    ProductCategoryId = responseBo.Bo.ProductCategoryId
                };
            }

            return responseDto;
        }

        //List<ProductCategoryListAdminDto> GetList(int parentId, bool getSubList)
        //{
        //    List<ProductCategoryListAdminDto> list =
        //        (from x in Business.Stc.ProductCategoryList
        //         where x.ParentId == parentId
        //         select new ProductCategoryListAdminDto
        //         {
        //             Id = x.Id,
        //             Name = x.Name,

        //             SubCategoryList = getSubList ? GetList(x.Id, getSubList) : null
        //         }).ToList();

        //    return list;
        //}
    }
}