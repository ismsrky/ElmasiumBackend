using Mh.Business.Bo.IncludeExclude;
using Mh.Business.Bo.Person.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.IncludeExclude;
using Mh.Service.Dto.IncludeExclude;
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

namespace Mh.Service.WebApi.IncludeExclude
{
    public class IncludeExcludeController : BaseController
    {
        readonly IIncludeExcludeBusiness ieBusiness;
        public IncludeExcludeController(IIncludeExcludeBusiness _ieBusiness)
        {
            ieBusiness = _ieBusiness;
        }

        [HttpPost]
        public ResponseDto Save(IncludeExcludeSaveDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            IncludeExcludeSaveBo saveBo = new IncludeExcludeSaveBo()
            {
                ProductCategoryId = saveDto.ProductCategoryId,
                IsInclude= saveDto.IsInclude,

                IncludeExcludeName = saveDto.IncludeExcludeName,
                IncludeExcludeNameListStr = null,

                Session = Session
            };

            ResponseBo responseBo = ieBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        [Admin]
        /// <summary>
        /// This method is smilar to 'Save' in 'PropertyController.cs'
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        public ResponseDto SaveAll(IncludeExcludeSaveDto saveDto)
        {
            //if(Session.RealPerson.Id)
            ResponseDto responseDto = new ResponseDto();

            IncludeExcludeSaveBo saveBo = new IncludeExcludeSaveBo()
            {
                ProductCategoryId = saveDto.ProductCategoryId,
                IsInclude = saveDto.IsInclude,

                IncludeExcludeName = saveDto.IncludeExcludeName,
                IncludeExcludeNameListStr = saveDto.IncludeExcludeNameListStr,

                Session = Session
            };

            bool isValid = true;

            try
            {
                string[] listStr = null;
                if (saveBo.IncludeExcludeNameListStr.IsNull())
                {
                    isValid = false;
                }
                else
                {
                    listStr = saveBo.IncludeExcludeNameListStr.Split('\n');
                    if (listStr.IsNull() || listStr.Length == 0)
                    {
                        isValid = false;
                    }
                }

                List<string> nabersin = new List<string>();
                if (isValid)
                {
                    saveBo.IncludeExcludeNameListStr = "";

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
                        saveBo.IncludeExcludeNameListStr += item + "\n";
                    }

                    saveBo.IncludeExcludeNameListStr = saveBo.IncludeExcludeNameListStr.Substring(0, saveBo.IncludeExcludeNameListStr.Length - 1);
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

            ResponseBo responseBo = ieBusiness.Save(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto SavePersonProduct(PersonProductIncludeExcludeDto saveDto)
        {
            ResponseDto responseDto = new ResponseDto();

            PersonProductIncludeExcludeBo saveBo = new PersonProductIncludeExcludeBo()
            {
                PersonProductId = saveDto.PersonProductId,
                IsInclude = saveDto.IsInclude,

                IncludeExcludeList = saveDto.IncludeExcludeList == null ? null :
                              (from x in saveDto.IncludeExcludeList
                               select new IncludeExcludeBo()
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   PriceGap = x.PriceGap
                               }).ToList(),

                Session = Session
            };

            ResponseBo responseBo = ieBusiness.SavePersonProduct(saveBo);
            responseDto = responseBo.ToResponseDto();

            return responseDto;
        }

        [HttpPost]
        public ResponseDto<List<IncludeExcludeDto>> GetList(IncludeExcludeGetListCriteriaDto criteriaDto)
        {
            IncludeExcludeGetListCriteriaBo criteriaBo = new IncludeExcludeGetListCriteriaBo()
            {
                CaseId = criteriaDto.CaseId,
                IsInclude = criteriaDto.IsInclude,

                ProductCategoryId = criteriaDto.ProductCategoryId,
                Name = criteriaDto.Name,
                PageOffSet = criteriaDto.PageOffSet,

                PersonProductId = criteriaDto.PersonProductId,

                Session = Session
            };

            ResponseBo<List<IncludeExcludeBo>> responseBo = ieBusiness.GetList(criteriaBo);

            ResponseDto<List<IncludeExcludeDto>> responseDto = responseBo.ToResponseDto<List<IncludeExcludeDto>, List<IncludeExcludeBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<IncludeExcludeDto>();
                foreach (IncludeExcludeBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new IncludeExcludeDto()
                    {
                        Id = itemBo.Id,
                        Name = itemBo.Name,
                        PriceGap = itemBo.PriceGap,

                        IsInclude = itemBo.IsInclude
                    });
                }

            }

            return responseDto;
        }
    }
}