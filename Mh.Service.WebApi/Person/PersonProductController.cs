using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Product.Category;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Person;
using Mh.Business.Product;
using Mh.Service.Dto.Person.Product;
using Mh.Service.Dto.Product.Category;
using Mh.Service.Dto.Product.Code;
using Mh.Service.Dto.Product.Price;
using Mh.Service.Dto.Sys;
using Mh.Service.WebApi.Product;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Person
{
    public class PersonProductController : BaseController
    {
        readonly IPersonProductBusiness personProductBusiness;

        public PersonProductController(IPersonProductBusiness _personProductBusiness)
        {
            personProductBusiness = _personProductBusiness;
        }

        [HttpPost]
        public ResponseDto<PersonProductDto> Get(PersonProductGetCriteriaDto criteriaDto)
        {
            PersonProductGetCriteriaBo criteriaBo = new PersonProductGetCriteriaBo()
            {
                PersonProductId = criteriaDto.PersonProductId,

                ProductId = criteriaDto.ProductId,
                ProductCode = criteriaDto.ProductCode,

                PersonId = criteriaDto.PersonId,

                CurrencyId = criteriaDto.CurrencyId,

                Session = Session
            };

            ResponseBo<PersonProductBo> responseBo = personProductBusiness.Get(criteriaBo);

            if (criteriaBo.ProductCode != null && criteriaBo.ProductId == null && responseBo.IsSuccess && responseBo.Bo == null)
            {
                using (ProductController productController = new ProductController(new ProductBusiness()))
                {
                    ResponseDto t_responseDto = productController.AddFromExternalSource(criteriaBo.ProductCode, Session, Session.MyPerson.Id);

                    if (t_responseDto.IsSuccess)
                    {
                        responseBo = personProductBusiness.Get(criteriaBo);
                    }
                }
            }

            ResponseDto<PersonProductDto> responseDto = responseBo.ToResponseDto<PersonProductDto, PersonProductBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonProductDto()
                {
                    Id = responseBo.Bo.Id,
                    PurchaseVatRate = responseBo.Bo.PurchaseVatRate,
                    SaleVatRate = responseBo.Bo.SaleVatRate,
                    CategoryId = responseBo.Bo.CategoryId,
                    Balance = responseBo.Bo.Balance,

                    ProductId = responseBo.Bo.ProductId,
                    ProductName = responseBo.Bo.ProductName,
                    ProductTypeId = responseBo.Bo.ProductTypeId,

                    PortraitImageUniqueIdStr = responseBo.Bo.PortraitImageUniqueId == null ? null : responseBo.Bo.PortraitImageUniqueId.ToStringNull().ToUpperNull()
                        + "." + responseBo.Bo.PortraitImageFileTypeId.ToStringNull().ToLowerNull(),

                    // itemBo.CodeList should not be null
                    // at least there must be an item that contaions product Id value.
                    CodeList = (from pc in responseBo.Bo.CodeList
                                select new ProductCodeDto
                                {
                                    Code = pc.Code,
                                    ProductCodeTypeId = pc.ProductCodeTypeId
                                }).ToList(),

                    // itemBo.PriceList should not be null
                    // every product must have price in every currency.
                    // if a price was not specified in given currency, bussiness will convert the price to given currency.
                    Price = new ProductPriceDto
                    {
                        PurhasePrice = responseBo.Bo.Price.PurhasePrice,
                        SalePrice = responseBo.Bo.Price.SalePrice,
                        OnlineSalePrice = responseBo.Bo.Price.OnlineSalePrice,

                        CurrencyId = responseBo.Bo.Price.CurrencyId,

                        FromPool = responseBo.Bo.Price.FromPool
                    }
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Delete(PersonProductDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductDeleteBo deleteBo = new PersonProductDeleteBo()
            {
                Id = deleteDto.Id,

                Session = Session
            };

            ResponseBo responseBo = personProductBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonProductListDto>> GetList(PersonProductGetListCriteriaDto criteriaDto)
        {
            PersonProductGetListCriteriaBo criteriaBo = new PersonProductGetListCriteriaBo()
            {
                ProductNameCode = criteriaDto.ProductNameCode,
                ProductTypeId = criteriaDto.ProductTypeId,

                StockStatId = criteriaDto.StockStatId,

                PersonId = criteriaDto.PersonId,
                CurrencyId = criteriaDto.CurrencyId,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<PersonProductListBo>> responseBo = personProductBusiness.GetList(criteriaBo);

            ResponseDto<List<PersonProductListDto>> responseDto = responseBo.ToResponseDto<List<PersonProductListDto>, List<PersonProductListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonProductListDto>();
                foreach (PersonProductListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonProductListDto()
                    {
                        Id = itemBo.Id,

                        ProductId = itemBo.ProductId,
                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,

                        VateRate = itemBo.VateRate,

                        Balance = itemBo.Balance,

                        PortraitImageUniqueIdStr = base.GetImageName(itemBo.PortraitImageUniqueId, itemBo.PortraitImageFileTypeId),

                        CodeList = (from x in itemBo.CodeList
                                    select new ProductCodeDto()
                                    {
                                        Code = x.Code,
                                        ProductCodeTypeId = x.ProductCodeTypeId,
                                        ProductId = x.ProductId
                                    }).ToList(),

                        Price = itemBo.Price == null ? null :
                        new ProductPriceDto()
                        {
                            PurhasePrice = itemBo.Price.PurhasePrice,
                            SalePrice = itemBo.Price.SalePrice,
                            OnlineSalePrice = itemBo.Price.OnlineSalePrice,

                            CurrencyId = itemBo.Price.CurrencyId,

                            FromPool = itemBo.Price.FromPool
                        }
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<PersonProductGeneralDto> GetGeneral(PersonProductGeneralGetCriteriaDto criteriaDto)
        {
            PersonProductGeneralGetCriteriaBo criteriaBo = new PersonProductGeneralGetCriteriaBo()
            {
                PersonProductId = criteriaDto.PersonProductId,

                Session = Session
            };

            ResponseBo<PersonProductGeneralBo> responseBo = personProductBusiness.GetGeneral(criteriaBo);

            ResponseDto<PersonProductGeneralDto> responseDto = responseBo.ToResponseDto<PersonProductGeneralDto, PersonProductGeneralBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonProductGeneralDto()
                {
                    DefaultCurrencyId = responseBo.Bo.DefaultCurrencyId,

                    PurchaseVatRate = responseBo.Bo.PurchaseVatRate,
                    SaleVatRate = responseBo.Bo.SaleVatRate,

                    PurhasePrice = responseBo.Bo.PurhasePrice,
                    SalePrice = responseBo.Bo.SalePrice,
                    OnlineSalePrice = responseBo.Bo.OnlineSalePrice,

                    IsTemporarilyUnavaible = responseBo.Bo.IsTemporarilyUnavaible,
                    IsSaleForOnline = responseBo.Bo.IsSaleForOnline,
                    Notes = responseBo.Bo.Notes
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto AddToInventory(PersonProductAddInventoryDto addDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductAddInventoryBo addBo = new PersonProductAddInventoryBo()
            {
                ProductId = addDto.ProductId,
                PersonId = addDto.PersonId,

                Session = Session
            };

            ResponseBo responseBo = personProductBusiness.AddToInventory(addBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto Update(PersonProductUpdateDto updateDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductUpdateBo updateBo = new PersonProductUpdateBo()
            {
                PersonProductId = updateDto.PersonProductId,
                ProductUpdateTypeList = updateDto.ProductUpdateTypeList,

                Name = updateDto.Name,

                CategoryId = updateDto.CategoryId,

                PurchaseVatRate = updateDto.PurchaseVatRate,
                SaleVatRate = updateDto.SaleVatRate,

                PurhasePrice = updateDto.PurhasePrice,
                SalePrice = updateDto.SalePrice,
                OnlineSalePrice = updateDto.OnlineSalePrice,

                IsTemporarilyUnavaible = updateDto.IsTemporarilyUnavaible,
                IsSaleForOnline = updateDto.IsSaleForOnline,
                Notes = updateDto.Notes,

                Session = Session
            };

            ResponseBo responseBo = personProductBusiness.Update(updateBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonProductActivityListDto>> GetActivityList(PersonProductActivityGetListCriteriaDto criteriaDto)
        {
            PersonProductActivityGetListCriteriaBo criteriaBo = new PersonProductActivityGetListCriteriaBo()
            {
                OwnerPersonId = criteriaDto.OwnerPersonId,
                ProductIdList = criteriaDto.ProductIdList,

                ApprovalStatIdList = criteriaDto.ApprovalStatIdList,

                QuantityTotalMin = criteriaDto.QuantityTotalMin,
                QuantityTotalMax = criteriaDto.QuantityTotalMax,

                IssueDateStart = criteriaDto.IssueDateStartNumber.ToDateTimeFromNumberNull(),
                IssueDateEnd = criteriaDto.IssueDateEndNumber.ToDateTimeFromNumberNull(),

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<PersonProductActivityListBo>> responseBo = personProductBusiness.GetActivityList(criteriaBo);

            ResponseDto<List<PersonProductActivityListDto>> responseDto = responseBo.ToResponseDto<List<PersonProductActivityListDto>, List<PersonProductActivityListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonProductActivityListDto>();
                foreach (PersonProductActivityListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonProductActivityListDto()
                    {
                        Id = itemBo.Id,
                        Quantity = itemBo.Quantity,

                        ProductId = itemBo.ProductId,
                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,
                        IsDebt = itemBo.IsDebt,

                        OwnerPersonId = itemBo.OwnerPersonId,

                        FicheId = itemBo.FicheId,
                        FicheCurrencyId = itemBo.FicheCurrencyId,
                        FicheApprovalStatId = itemBo.FicheApprovalStatId,
                        FicheIssueDateNumber = itemBo.FicheIssueDate.ToNumberFromDateTime()
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<PersonProductProfileDto> GetProfile(PersonProductProfileGetCriteriaDto criteriaDto)
        {
            PersonProductProfileGetCriteriaBo criteriaBo = new PersonProductProfileGetCriteriaBo
            {
                CaseId = criteriaDto.CaseId,

                PersonUrlName = criteriaDto.PersonUrlName,
                ProductCode = criteriaDto.ProductCode,

                PersonProductId = criteriaDto.PersonProductId,

                Session = Session
            };

            ResponseBo<PersonProductProfileBo> responseBo = personProductBusiness.GetProfile(criteriaBo);

            ResponseDto<PersonProductProfileDto> responseDto = responseBo.ToResponseDto<PersonProductProfileDto, PersonProductProfileBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonProductProfileDto()
                {
                    PersonProductId = responseBo.Bo.PersonProductId,
                    CategoryId = responseBo.Bo.CategoryId,
                    OnlineSalePrice = responseBo.Bo.OnlineSalePrice,
                    SalePrice = responseBo.Bo.SalePrice,
                    StarCount = responseBo.Bo.StarCount,
                    StarSum = responseBo.Bo.StarSum,
                    Balance = responseBo.Bo.Balance,
                    Notes = responseBo.Bo.Notes,
                    IsSaleForOnline = responseBo.Bo.IsSaleForOnline,
                    IsTemporarilyUnavaible = responseBo.Bo.IsTemporarilyUnavaible,

                    IsShopOwner = responseBo.Bo.IsShopOwner,

                    ProductId = responseBo.Bo.ProductId,
                    ProductTypeId = responseBo.Bo.ProductTypeId,
                    ProductName = responseBo.Bo.ProductName,

                    ShopId = responseBo.Bo.ShopId,
                    ShopFullName = responseBo.Bo.ShopFullName,
                    ShopStarCount = responseBo.Bo.ShopStarCount,
                    ShopStarSum = responseBo.Bo.ShopStarSum,
                    ShopDefaultCurrencyId = responseBo.Bo.ShopDefaultCurrencyId,
                    ShopUrlName = responseBo.Bo.ShopUrlName,
                    ShopIsAvailable = responseBo.Bo.ShopIsAvailable,
                    ShopTypeName = responseBo.Bo.ShopTypeName,

                    PortraitImageUniqueIdStr = base.GetImageName(responseBo.Bo.PortraitImageUniqueId, responseBo.Bo.PortraitImageFileTypeId),

                    CodeList = (from x in responseBo.Bo.CodeList
                                select new ProductCodeDto()
                                {
                                    Code = x.Code,
                                    ProductCodeTypeId = x.ProductCodeTypeId,
                                    ProductId = x.ProductId
                                }).ToList()
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<PersonProductSeePriceDto> GetSeePrice(PersonProductSeePriceGetCriteriaDto criteriaDto)
        {
            PersonProductSeePriceGetCriteriaBo criteriaBo = new PersonProductSeePriceGetCriteriaBo()
            {
                ProductId = criteriaDto.ProductId,
                ProductCode = criteriaDto.ProductCode,

                ShopId = criteriaDto.ShopId,

                CurrencyId = criteriaDto.CurrencyId,

                Session = Session
            };

            ResponseBo<PersonProductSeePriceBo> responseBo = personProductBusiness.GetSeePrice(criteriaBo);

            ResponseDto<PersonProductSeePriceDto> responseDto = responseBo.ToResponseDto<PersonProductSeePriceDto, PersonProductSeePriceBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new PersonProductSeePriceDto()
                {
                    ProductId = responseBo.Bo.ProductId,
                    ProductName = responseBo.Bo.ProductName,
                    ProductTypeId = responseBo.Bo.ProductTypeId,

                    PersonProductId = responseBo.Bo.PersonProductId,

                    PortraitImageUniqueIdStr = responseBo.Bo.PortraitImageUniqueId == null ? null : responseBo.Bo.PortraitImageUniqueId.ToStringNull().ToUpperNull()
                        + "." + responseBo.Bo.PortraitImageFileTypeId.ToStringNull().ToLowerNull(),

                    // itemBo.CodeList should not be null
                    // at least there must be an item that contaions product Id value.
                    CodeList = (from pc in responseBo.Bo.CodeList
                                select new ProductCodeDto
                                {
                                    Code = pc.Code,
                                    ProductCodeTypeId = pc.ProductCodeTypeId
                                }).ToList(),

                    // itemBo.PriceList should not be null
                    // every product must have price in every currency.
                    // if a price was not specified in given currency, bussiness will convert the price to given currency.
                    Price = new ProductPriceDto
                    {
                        PurhasePrice = -1, // do not show purhase price in see price screen.
                        SalePrice = responseBo.Bo.Price.SalePrice,
                        OnlineSalePrice = responseBo.Bo.Price.OnlineSalePrice,

                        CurrencyId = responseBo.Bo.Price.CurrencyId,

                        FromPool = responseBo.Bo.Price.FromPool
                    }
                };
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<ProductCategoryListDto>> GetCategoryList(PersonProductCategoryGetListCriteriaDto criteriaDto)
        {
            PersonProductCategoryGetListCriteriaBo criteriaBo = new PersonProductCategoryGetListCriteriaBo()
            {
                ProductTypeId = criteriaDto.ProductTypeId,
                PersonId = criteriaDto.PersonId,

                IsSaleForOnline = criteriaDto.IsSaleForOnline,
                IsTemporarilyUnavaible = criteriaDto.IsTemporarilyUnavaible,

                StockStatId = criteriaDto.StockStatId,

                ProductNameCode = criteriaDto.ProductNameCode,

                Session = Session
            };

            ResponseBo<List<ProductCategoryListBo>> responseBo = personProductBusiness.GetCategoryList(criteriaBo);

            ResponseDto<List<ProductCategoryListDto>> responseDto = responseBo.ToResponseDto<List<ProductCategoryListDto>, List<ProductCategoryListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<ProductCategoryListDto>();

                foreach (ProductCategoryListBo itemBo in responseBo.Bo.Where(x => x.IsLast))
                {
                    responseDto.Dto.Add(new ProductCategoryListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name, //+ " - " + GetFullName(itemBo, responseBo.Bo),

                        UrlName = itemBo.UrlName,

                        IsLast = itemBo.IsLast
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PersonProfileProductListDto>> GetListForProfile(PersonProfileProductGetListCriteriaDto criteriaDto)
        {
            PersonProfileProductGetListCriteriaBo criteriaBo = new PersonProfileProductGetListCriteriaBo()
            {
                PersonProductId = criteriaDto.PersonProductId,

                ShopId = criteriaDto.ShopId,
                CurrencyId = criteriaDto.CurrencyId,

                CategoryId = criteriaDto.CategoryId,
                StockStatId = criteriaDto.StockStatId,

                IsSaleForOnline = criteriaDto.IsSaleForOnline,
                IsTemporarilyUnavaible = criteriaDto.IsTemporarilyUnavaible,

                ProductNameCode = criteriaDto.ProductNameCode,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            ResponseBo<List<PersonProfileProductListBo>> responseBo = personProductBusiness.GetListForProfile(criteriaBo);

            ResponseDto<List<PersonProfileProductListDto>> responseDto = responseBo.ToResponseDto<List<PersonProfileProductListDto>, List<PersonProfileProductListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PersonProfileProductListDto>();
                foreach (PersonProfileProductListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PersonProfileProductListDto()
                    {
                        Id = itemBo.Id,

                        ProductId = itemBo.ProductId,
                        ProductName = itemBo.ProductName,
                        ProductTypeId = itemBo.ProductTypeId,

                        OnlineSalePrice = itemBo.OnlineSalePrice,

                        StarCount = itemBo.StarCount,
                        StarSum = itemBo.StarSum,

                        SaleVatRate = itemBo.SaleVatRate,
                        Balance = itemBo.Balance,
                        IsSaleForOnline = itemBo.IsSaleForOnline,
                        IsTemporarilyUnavaible = itemBo.IsTemporarilyUnavaible,
                        Notes = itemBo.Notes,

                        PortraitImageUniqueIdStr = base.GetImageName(itemBo.PortraitImageUniqueId, itemBo.PortraitImageFileTypeId)                        
                    });
                }
            }

            return responseDto;
        }
    }
}