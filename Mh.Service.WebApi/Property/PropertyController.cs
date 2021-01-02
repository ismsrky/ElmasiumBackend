using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Property;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Property;
using Mh.Service.Dto.Person.Product;
using Mh.Service.Dto.Property;
using Mh.Service.Dto.Sys;
using Mh.Service.WebApi.Attribute;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mh.Service.WebApi.Property
{
    public class PropertyController : BaseController
    {
        readonly IPropertyBusiness propertyBusiness;
        public PropertyController(IPropertyBusiness _propertyBusiness)
        {
            propertyBusiness = _propertyBusiness;
        }

        [HttpPost]
        //[Admin]
        public ResponseDto<List<PropertyListDto>> GetList(PropertyGetListCriteriaDto criteriaDto)
        {
            PropertyGetListCriteriaBo criteriaBo = new PropertyGetListCriteriaBo()
            {
                CaseId = criteriaDto.CaseId,

                ProductCategoryId = criteriaDto.ProductCategoryId,
                PersonProductId = criteriaDto.PersonProductId,
                PropertyGroupId = criteriaDto.PropertyGroupId,

                Session = Session
            };

            ResponseBo<List<PropertyListBo>> responseBo = propertyBusiness.GetList(criteriaBo);

            ResponseDto<List<PropertyListDto>> responseDto = responseBo.ToResponseDto<List<PropertyListDto>, List<PropertyListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PropertyListDto>();

                // CaseId: 0: get list by category, 1: get list by person product, 2: get list by group.
                if (criteriaDto.CaseId == 2)
                {
                    responseDto.Dto = new List<PropertyListDto>();
                    foreach (PropertyListBo itemBo in responseBo.Bo)
                    {
                        responseDto.Dto.Add(new PropertyListDto()
                        {
                            Id = itemBo.PropertyId,
                            Name = itemBo.PropertyName,
                            UrlName = itemBo.PropertyUrlName
                        });
                    }
                }
                else
                {
                    responseDto.Dto = responseBo.Bo.
                    GroupBy(x => x.GroupId).Select(x => x.First()).
                    Select(
                    x => new PropertyListDto()
                    {
                        Id = x.GroupId,
                        Name = x.GroupName,
                        UrlName = x.GroupUrlName,
                        PropertyList = responseBo.Bo.Where(y => y.GroupId == x.GroupId).Select(
                               y => new PropertyListDto()
                               {
                                   Id = y.PropertyId,
                                   Name = y.PropertyName,
                                   UrlName = y.PropertyUrlName
                               }).ToList()
                    }).ToList();
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<PropertyGroupListDto>> GetGroupList(PropertyGroupGetListCriteriaDto criteriaDto)
        {
            PropertyGroupGetListCriteriaBo criteriaBo = new PropertyGroupGetListCriteriaBo()
            {
                ProductCategoryId = criteriaDto.ProductCategoryId,

                Session = Session
            };

            ResponseBo<List<PropertyGroupListBo>> responseBo = propertyBusiness.GetGroupList(criteriaBo);

            ResponseDto<List<PropertyGroupListDto>> responseDto = responseBo.ToResponseDto<List<PropertyGroupListDto>, List<PropertyGroupListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<PropertyGroupListDto>();
                foreach (PropertyGroupListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new PropertyGroupListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        UrlName = itemBo.UrlName,
                        CanFilter = itemBo.CanFilter
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        [Admin]
        /// <summary>
        /// This method is smilar to 'Save' in 'OptionController.cs'
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        public ResponseDto Save(PropertySaveDto saveDto)
        {
            //if(Session.RealPerson.Id)
            ResponseDto responseDto = new ResponseDto();

            PropertySaveBo saveBo = new PropertySaveBo()
            {
                ProductCategoryId = saveDto.ProductCategoryId,
                Name = saveDto.Name,
                PropertyNameListStr = saveDto.PropertyNameListStr,

                Session = Session
            };

            bool isValid = true;

            try
            {
                string[] listStr = null;
                if (saveBo.PropertyNameListStr.IsNull())
                {
                    isValid = false;
                }
                else
                {
                    listStr = saveBo.PropertyNameListStr.Split('\n');
                    if (listStr.IsNull() || listStr.Length == 0)
                    {
                        isValid = false;
                    }
                }

                List<string> nabersin = new List<string>();
                if (isValid)
                {
                    saveBo.PropertyNameListStr = "";

                    string itemStr = null;
                    List<string> tlistStr = new List<string>();
                    foreach (var item in listStr)
                    {
                        if (item.IsNull() || item.Trim().IsNull()) continue;

                        itemStr = item.Substring(0, item.IndexOf("(")).Trim();

                        if (tlistStr.FirstOrDefault(f => string.Equals(f.ToLower(new CultureInfo("tr-Tr", false)), itemStr.ToLower(new CultureInfo("tr-Tr", false)), StringComparison.CurrentCultureIgnoreCase)).IsNotNull())
                        {
                            nabersin.Add(itemStr);
                            continue;
                        }

                        tlistStr.Add(itemStr);
                        saveBo.PropertyNameListStr += itemStr + "\n";
                    }

                    saveBo.PropertyNameListStr = saveBo.PropertyNameListStr.Substring(0, saveBo.PropertyNameListStr.Length - 1);
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            if (!isValid)
            {
                return new ResponseDto()
                {
                    Message = "Geçersiz format",
                    IsSuccess = false
                };
            }

            ResponseBo responseBo = propertyBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SavePersonProduct(PersonProductPropertyDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductPropertyBo saveBo = new PersonProductPropertyBo()
            {
                PersonProductId = saveDto.PersonProductId,
                PropertyId = saveDto.PropertyId,

                Session = Session
            };

            ResponseBo responseBo = propertyBusiness.SavePersonProduct(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto DeletePersonProduct(PersonProductPropertyDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductPropertyBo deleteBo = new PersonProductPropertyBo()
            {
                PersonProductId = deleteDto.PersonProductId,
                PropertyId = deleteDto.PropertyId,

                Session = Session
            };

            ResponseBo responseBo = propertyBusiness.DeletePersonProduct(deleteBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}