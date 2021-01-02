using Mh.Business.Bo.Product.Filter;
using Mh.Business.Bo.Property;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Product;
using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Product.Filter;
using Mh.Service.Dto.Property;
using Mh.Service.Dto.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mh.Service.WebApi.Product
{
    public class ProductFilterController : BaseController
    {
        readonly IProductFilterBusiness productFilterBusiness;

        public ProductFilterController(IProductFilterBusiness _productFilterBusiness)
        {
            productFilterBusiness = _productFilterBusiness;
        }

        [HttpPost]
        public ResponseDto<List<ProductFilterListDto>> GetList(ProductFilterGetListCriteriaDto criteriaDto)
        {
            ProductFilterGetListCriteriaBo criteriaBo = new ProductFilterGetListCriteriaBo()
            {
                SearchWord = criteriaDto.SearchWord,
                ProductCategoryId = criteriaDto.ProductCategoryId,
                PropertyList = criteriaDto.PropertyList,
                MinPrice = criteriaDto.MinPrice,

                MaxPrice = criteriaDto.MaxPrice,

                PageNumber = criteriaDto.PageNumber,

                Session = Session
            };

            ResponseBo<List<ProductFilterListBo>> responseBo = productFilterBusiness.GetList(criteriaBo);

            ResponseDto<List<ProductFilterListDto>> responseDto = responseBo.ToResponseDto<List<ProductFilterListDto>, List<ProductFilterListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ProductFilterListDto>();
                foreach (ProductFilterListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ProductFilterListDto()
                    {
                        PersonProductId = itemBo.PersonProductId,
                        OnlineSalePrice = itemBo.OnlineSalePrice,

                        StarCount = itemBo.StarCount,
                        StarSum = itemBo.StarSum,

                        Notes = itemBo.Notes,

                        ProductId = itemBo.ProductId,
                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,

                        ShopId = itemBo.ShopId,
                        ShopFullName = itemBo.ShopFullName,
                        ShopUrlName = itemBo.ShopUrlName,
                        ShopStarCount = itemBo.ShopStarCount,
                        ShopStarSum = itemBo.ShopStarSum,
                        ShopTypeName = itemBo.ShopTypeName
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ProductFilterListExtraDto> GetListExtra(ProductFilterGetListExtraCriteriaDto criteriaDto)
        {
            //System.Threading.Thread.Sleep(3000);
            ProductFilterGetListExtraCriteriaBo criteriaBo = new ProductFilterGetListExtraCriteriaBo()
            {
                PersonProductId = criteriaDto.PersonProductId,

                Session = Session
            };

            ResponseBo<ProductFilterListExtraBo> responseBo = productFilterBusiness.GetListExtra(criteriaBo);

            ResponseDto<ProductFilterListExtraDto> responseDto = responseBo.ToResponseDto<ProductFilterListExtraDto, ProductFilterListExtraBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ProductFilterListExtraDto()
                {
                    PersonProductId = responseBo.Bo.PersonProductId,
                    PortraitImageUniqueIdStr = base.GetImageName(responseBo.Bo.PortraitImageUniqueId, responseBo.Bo.PortraitImageFileTypeId)
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<ProductFilterListSummaryDto> GetListSummary(ProductFilterGetListCriteriaDto criteriaDto)
        {
            ProductFilterGetListCriteriaBo criteriaBo = new ProductFilterGetListCriteriaBo()
            {
                SearchWord = criteriaDto.SearchWord,
                ProductCategoryId = criteriaDto.ProductCategoryId,
                PropertyList = criteriaDto.PropertyList,

                MinPrice = criteriaDto.MinPrice,
                MaxPrice = criteriaDto.MaxPrice,

                Session = Session
            };

            ResponseBo<ProductFilterListSummaryBo> responseBo = productFilterBusiness.GetListSummary(criteriaBo);

            ResponseDto<ProductFilterListSummaryDto> responseDto = responseBo.ToResponseDto<ProductFilterListSummaryDto, ProductFilterListSummaryBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new ProductFilterListSummaryDto();

                responseDto.Dto.Count = responseBo.Bo.Count;
                responseDto.Dto.PageSize = responseBo.Bo.PageSize;

                if (responseBo.Bo.PropertyList != null)
                {
                    responseDto.Dto.PropertyList = responseBo.Bo.PropertyList.
                    GroupBy(x => x.GroupId).Select(x => x.First()).
                    Select(
                    x => new PropertyListDto()
                    {
                        Id = x.GroupId,
                        Name = x.GroupName,
                        UrlName = x.GroupUrlName,
                        PropertyList = responseBo.Bo.PropertyList.Where(y => y.GroupId == x.GroupId).Select(
                               y => new PropertyListDto()
                               {
                                   Id = y.PropertyId,
                                   Name = y.PropertyName,
                                   UrlName = y.PropertyUrlName,
                                   Count = y.PropertyCount
                               }).ToList()
                    }).ToList();

                    //responseDto.Dto.PropertyCountList = new List<ProductFilterListCountDto>();
                    //foreach (PropertyListBo item in responseBo.Bo.PropertyList)
                    //{
                    //    responseDto.Dto.PropertyCountList.Add(new ProductFilterListCountDto()
                    //    {
                    //        PropertyId = item.PropertyId,
                    //        Cnt = item.Cnt
                    //    });
                    //}
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<ProductFilterPoolListDto>> GetPoolList(ProductFilterPoolGetListCriteriaDto criteriaDto)
        {
            ProductFilterPoolGetListCriteriaBo criteriaBo = new ProductFilterPoolGetListCriteriaBo()
            {
                ProductNameCode = criteriaDto.ProductNameCode,
                ProductTypeId = criteriaDto.ProductTypeId,
                ProductCategoryId = criteriaDto.ProductCategoryId,

                OnlyInInventory = criteriaDto.OnlyInInventory,
                OnlyInStock = criteriaDto.OnlyInStock,

                PersonId = criteriaDto.PersonId,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<ProductFilterPoolListBo>> responseBo = productFilterBusiness.GetPoolList(criteriaBo);

            ResponseDto<List<ProductFilterPoolListDto>> responseDto = responseBo.ToResponseDto<List<ProductFilterPoolListDto>, List<ProductFilterPoolListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ProductFilterPoolListDto>();
                foreach (ProductFilterPoolListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new ProductFilterPoolListDto()
                    {
                        ProductId = itemBo.ProductId,
                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,

                        PortraitImageUniqueIdStr = base.GetImageName(itemBo.PortraitImageUniqueId, itemBo.PortraitImageFileTypeId),

                        CategoryId = itemBo.CategoryId,
                        ProductCodeList = (from x in itemBo.ProductCodeList // cannot be null
                                           select new ProductCodeDto
                                           {
                                               ProductCodeTypeId = x.ProductCodeTypeId,
                                               Code = x.Code
                                           }).ToList(),

                        PersonProductId = itemBo.PersonProductId,
                        Balance = itemBo.Balance
                    });
                }
            }

            return responseDto;
        }
    }
}