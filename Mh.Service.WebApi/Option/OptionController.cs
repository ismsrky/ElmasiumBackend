using Mh.Business.Bo.Option;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Option;
using Mh.Service.Dto.Option;
using Mh.Service.Dto.Person.Product;
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

namespace Mh.Service.WebApi.Option
{
    public class OptionController : BaseController
    {
        readonly IOptionBusiness optionBusiness;
        public OptionController(IOptionBusiness _optionBusiness)
        {
            optionBusiness = _optionBusiness;
        }

        [HttpPost]
        public ResponseDto<List<OptionListDto>> GetList(OptionGetListCriteriaDto criteriaDto)
        {
            OptionGetListCriteriaBo criteriaBo = new OptionGetListCriteriaBo()
            {
                CaseId = criteriaDto.CaseId,

                ProductCategoryId = criteriaDto.ProductCategoryId,
                PersonProductId = criteriaDto.PersonProductId,
                OptionGroupId = criteriaDto.OptionGroupId,

                Session = Session
            };

            ResponseBo<List<OptionListBo>> responseBo = optionBusiness.GetList(criteriaBo);

            ResponseDto<List<OptionListDto>> responseDto = responseBo.ToResponseDto<List<OptionListDto>, List<OptionListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<OptionListDto>();

                // CaseId: 0: get list by category, 1: get list by person product, 2: get list by group.
                if (criteriaDto.CaseId == 2)
                {
                    responseDto.Dto = new List<OptionListDto>();
                    foreach (OptionListBo itemBo in responseBo.Bo)
                    {
                        responseDto.Dto.Add(new OptionListDto()
                        {
                            Id = itemBo.OptionId,
                            Name = itemBo.OptionName,
                            UrlName = itemBo.OptionUrlName,

                            PriceGap = itemBo.OptionPriceGap
                        });
                    }
                }
                else
                {
                    responseDto.Dto = responseBo.Bo.
                    GroupBy(x => x.GroupId).Select(x => x.First()).
                    Select(
                    x => new OptionListDto()
                    {
                        Id = x.GroupId,
                        Name = x.GroupName,
                        UrlName = x.GroupUrlName,
                        OptionList = responseBo.Bo.Where(y => y.GroupId == x.GroupId).Select(
                               y => new OptionListDto()
                               {
                                   Id = y.OptionId,
                                   Name = y.OptionName,
                                   UrlName = y.OptionUrlName,

                                   PriceGap = y.OptionPriceGap
                               }).ToList()
                    }).ToList();
                }
            }

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<OptionGroupListDto>> GetGroupList(OptionGroupGetListCriteriaDto criteriaDto)
        {
            OptionGroupGetListCriteriaBo criteriaBo = new OptionGroupGetListCriteriaBo()
            {
                ProductCategoryId = criteriaDto.ProductCategoryId,

                Session = Session
            };

            ResponseBo<List<OptionGroupListBo>> responseBo = optionBusiness.GetGroupList(criteriaBo);

            ResponseDto<List<OptionGroupListDto>> responseDto = responseBo.ToResponseDto<List<OptionGroupListDto>, List<OptionGroupListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<OptionGroupListDto>();
                foreach (OptionGroupListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new OptionGroupListDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        UrlName = itemBo.UrlName
                    });
                }
            }

            return responseDto;
        }

        [HttpPost]
        [Admin]
        /// <summary>
        /// This method is smilar to 'Save' in 'PropertyController.cs'
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        public ResponseDto Save(OptionSaveDto saveDto)
        {
            //if(Session.RealPerson.Id)
            ResponseDto responseDto = new ResponseDto();

            OptionSaveBo saveBo = new OptionSaveBo()
            {
                ProductCategoryId = saveDto.ProductCategoryId,
                Name = saveDto.Name,
                OptionNameListStr = saveDto.OptionNameListStr,

                Session = Session
            };

            bool isValid = true;

            try
            {
                string[] listStr = null;
                if (saveBo.OptionNameListStr.IsNull())
                {
                    isValid = false;
                }
                else
                {
                    listStr = saveBo.OptionNameListStr.Split('\n');
                    if (listStr.IsNull() || listStr.Length == 0)
                    {
                        isValid = false;
                    }
                }

                List<string> nabersin = new List<string>();
                if (isValid)
                {
                    saveBo.OptionNameListStr = "";

                    List<string> tlistStr = new List<string>();
                    foreach (var item in listStr)
                    {
                        if (item.IsNull() || item.Trim().IsNull()) continue;

                        if (tlistStr.FirstOrDefault(f => string.Equals(f.ToLower(new CultureInfo("tr-Tr", false)), item.ToLower(new CultureInfo("tr-Tr", false)), StringComparison.CurrentCultureIgnoreCase)).IsNotNull())
                        {
                            nabersin.Add(item);
                            continue;
                        }

                        tlistStr.Add(item);
                        saveBo.OptionNameListStr += item + "\n";
                    }

                    saveBo.OptionNameListStr = saveBo.OptionNameListStr.Substring(0, saveBo.OptionNameListStr.Length - 1);
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

            ResponseBo responseBo = optionBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SavePersonProduct(PersonProductOptionDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductOptionBo saveBo = new PersonProductOptionBo()
            {
                PersonProductId = saveDto.PersonProductId,
                OptionList = saveDto.OptionList == null ? null :
                              (from x in saveDto.OptionList
                               select new OptionBo()
                               {
                                   OptionId = x.OptionId,
                                   PriceGap = x.PriceGap
                               }).ToList(),

                Session = Session
            };

            ResponseBo responseBo = optionBusiness.SavePersonProduct(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }
    }
}