using Mh.Business.Bo.Fiche;
using Mh.Business.Bo.Fiche.Money;
using Mh.Business.Bo.Fiche.Product;
using Mh.Business.Bo.Fiche.Relation;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Fiche;
using Mh.Service.Dto.Fiche;
using Mh.Service.Dto.Fiche.Money;
using Mh.Service.Dto.Sys;
using Mh.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Mh.Service.WebApi.Fiche
{
    public class FicheController : BaseController
    {
        readonly IFicheBusiness ficheBusiness;
        public FicheController(IFicheBusiness _ficheBusiness)
        {
            ficheBusiness = _ficheBusiness;
        }


        [HttpPost]
        public ResponseDto<FicheDto> Get(FicheGetCriteriaDto criteriaDto)
        {
            FicheGetCriteriaBo criteriaBo = new FicheGetCriteriaBo()
            {
                MyPersonId = criteriaDto.MyPersonId,
                FicheId = criteriaDto.FicheId,

                Session = Session
            };

            ResponseBo<FicheBo> responseBo = ficheBusiness.Get(criteriaBo);

            ResponseDto<FicheDto> responseDto = responseBo.ToResponseDto<FicheDto, FicheBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new FicheDto()
                {
                    Id = responseBo.Bo.Id,
                    DebtPersonId = responseBo.Bo.DebtPersonId,
                    CreditPersonId = responseBo.Bo.CreditPersonId,

                    FicheTypeId = responseBo.Bo.FicheTypeId,
                    CurrencyId = responseBo.Bo.CurrencyId,
                    ApprovalStatId = responseBo.Bo.ApprovalStatId,
                    IncludingVat = responseBo.Bo.IncludingVat,

                    FicheContentId = responseBo.Bo.FicheContentId,
                    FicheContentGroupId = responseBo.Bo.FicheContentGroupId,

                    PrintedCode = responseBo.Bo.PrintedCode,
                    IssueDateNumber = responseBo.Bo.IssueDate.ToNumberFromDateTime(),

                    GrandTotal = responseBo.Bo.GrandTotal,
                    Total = responseBo.Bo.Total,
                    RowDiscountTotal = responseBo.Bo.RowDiscountTotal,

                    UnderDiscountRate = responseBo.Bo.UnderDiscountRate,
                    UnderDiscountTotal = responseBo.Bo.UnderDiscountTotal,

                    Notes = responseBo.Bo.Notes,

                    IsUncompleted = responseBo.Bo.ApprovalStatId == Enums.ApprovalStats.xUncompleted,

                    MoneyList = responseBo.Bo.MoneyList == null ? null :
                    (from x in responseBo.Bo.MoneyList
                     select new FicheMoneyDto
                     {
                         Id = x.Id,

                         DebtPersonAccountId = x.DebtPersonAccountId,
                         CreditPersonAccountId = x.CreditPersonAccountId,

                         Total = x.Total,

                         DebtPersonAccountTypeId = x.DebtPersonAccountTypeId,
                         CreditPersonAccountTypeId = x.CreditPersonAccountTypeId,

                         Notes = x.Notes
                     }).ToList()
                };
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto<List<FicheListDto>> GetList(FicheGetListCriteriaDto criteriaDto)
        {
            FicheGetListCriteriaBo criteriaBo = new FicheGetListCriteriaBo()
            {
                OtherPersonsIdList = criteriaDto.OtherPersonsIdList,
                FicheTypeFakeIdList = criteriaDto.FicheTypeFakeIdList,

                ApprovalStatIdList = criteriaDto.ApprovalStatIdList,
                FicheContentIdList = criteriaDto.FicheContentIdList,

                GrandTotalMin = criteriaDto.GrandTotalMin,
                GrandTotalMax = criteriaDto.GrandTotalMax,

                PrintedCode = criteriaDto.PrintedCode,

                IssueDateStart = criteriaDto.IssueDateStartNumber.ToDateTimeFromNumberNull(),
                IssueDateEnd = criteriaDto.IssueDateEndNumber.ToDateTimeFromNumberNull(),

                CurrencyId = criteriaDto.CurrencyId,

                PaymentStatId = criteriaDto.PaymentStatId,

                FicheId = criteriaDto.FicheId,

                FicheIdRelated = criteriaDto.FicheIdRelated,

                DebtPersonId = criteriaDto.DebtPersonId,
                CreditPersonId = criteriaDto.CreditPersonId,

                ExcludingFicheIdList = criteriaDto.ExcludingFicheIdList,

                PageOffSet = criteriaDto.PageOffSet,

                Session = Session
            };

            criteriaBo.PrintedCode = criteriaBo.PrintedCode.IsNull() ? null : criteriaBo.PrintedCode;
            if (criteriaBo.PrintedCode != null)
            {
                criteriaBo.PrintedCode = criteriaBo.PrintedCode.Trim();
            }

            ResponseBo<List<FicheListBo>> responseBo = ficheBusiness.GetList(criteriaBo);

            ResponseDto<List<FicheListDto>> responseDto = responseBo.ToResponseDto<List<FicheListDto>, List<FicheListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<FicheListDto>();
                foreach (FicheListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new FicheListDto()
                    {
                        Id = itemBo.Id,

                        DebtPersonId = itemBo.DebtPersonId,
                        DebtPersonFullName = itemBo.DebtPersonFullName,
                        DebtPersonTypeId = itemBo.DebtPersonTypeId,
                        DebtPersonIsAlone = itemBo.DebtPersonIsAlone,

                        CreditPersonId = itemBo.CreditPersonId,
                        CreditPersonFullName = itemBo.CreditPersonFullName,
                        CreditPersonTypeId = itemBo.CreditPersonTypeId,
                        CreditPersonIsAlone = itemBo.CreditPersonIsAlone,

                        FicheTypeId = itemBo.FicheTypeId,
                        CurrencyId = itemBo.CurrencyId,
                        ApprovalStatId = itemBo.ApprovalStatId,
                        IncludingVat = itemBo.IncludingVat,

                        PaymentStatId = itemBo.PaymentStatId,
                        PaidTotal = itemBo.PaidTotal,

                        FicheContentId = itemBo.FicheContentId,
                        FicheContentGroupId = itemBo.FicheContentGroupId,

                        PrintedCode = itemBo.PrintedCode,
                        IssueDateNumber = itemBo.IssueDate.ToNumberFromDateTime(),
                        DueDateNumber = itemBo.DueDate.ToNumberFromDateTimeNull(),

                        GrandTotal = itemBo.GrandTotal,
                        Total = itemBo.Total,
                        RowDiscountTotal = itemBo.RowDiscountTotal,

                        UnderDiscountRate = itemBo.UnderDiscountRate,
                        UnderDiscountTotal = itemBo.UnderDiscountTotal,

                        FicheTypeFakeId = itemBo.FicheTypeFakeId,

                        IsDebtor = itemBo.IsDebtor,

                        Notes = itemBo.Notes,

                        LastApprovalFicheHistoryParentPersonId = itemBo.LastApprovalFicheHistoryParentPersonId,
                        LastApprovalFicheHistoryChildPersonId = itemBo.LastApprovalFicheHistoryChildPersonId,
                        LastApprovalStatId = itemBo.LastApprovalStatId
                    });
                }
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto Save(FicheDto ficheDto)
        {
            ResponseDto responseDto = new ResponseDto();

            if (ficheDto == null)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = Business.Stc.GetDicValue("xInvalidData", Session.RealPerson.LanguageId);
                return responseDto;
            }

            if ((ficheDto.FicheTypeId == Enums.FicheTypes.xReceipt || ficheDto.FicheTypeId == Enums.FicheTypes.xInvoice)
                && (ficheDto.ProductList == null || ficheDto.ProductList.Count() == 0))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = Business.Stc.GetDicValue("xNoProduct", Session.RealPerson.LanguageId);
                return responseDto;
            }

            if (ficheDto.IssueDateNumber < 0 || (ficheDto.DueDateNumber != null && ficheDto.DueDateNumber.Value < 0))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = Business.Stc.GetDicValue("xInvalidDate", Session.RealPerson.LanguageId);
                return responseDto;
            }

            FicheBo ficheBo = new FicheBo()
            {
                Id = ficheDto.Id,
                DebtPersonId = ficheDto.DebtPersonId,
                CreditPersonId = ficheDto.CreditPersonId,

                FicheTypeId = ficheDto.FicheTypeId,
                CurrencyId = ficheDto.CurrencyId,
                // FicheStatId sp will decide it.
                IncludingVat = ficheDto.IncludingVat,

                PrintedCode = ficheDto.PrintedCode,
                IssueDate = ficheDto.IssueDateNumber.ToDateTimeFromNumber(),
                DueDate = ficheDto.DueDateNumber.ToDateTimeFromNumberNull(),

                UnderDiscountRate = ficheDto.UnderDiscountRate,
                UnderDiscountTotal = ficheDto.UnderDiscountTotal,

                FicheContentId = ficheDto.FicheContentId,
                FicheContentGroupId = ficheDto.FicheContentGroupId,

                Notes = ficheDto.Notes,

                AcceptorPersonId = ficheDto.AcceptorPersonId,

                OrderId = ficheDto.OrderId,

                IsUncompleted = ficheDto.IsUncompleted,

                GrandTotal = ficheDto.GrandTotal, // Will not be calculated in case of fiche type is 'xDebtCredit'.

                Session = Session

                // Business will calculate below values:
                // GrandTotal
                // Total
                // RowDiscountTotal
                // UnderDiscountTotal
                // VatTotal
            };

            if (ficheDto.MoneyList != null && ficheDto.MoneyList.Count > 0)
            {
                ficheBo.MoneyList = (from x in ficheDto.MoneyList
                                     select new FicheMoneyBo
                                     {
                                         Id = x.Id,
                                         DebtPersonAccountId = x.DebtPersonAccountId,
                                         CreditPersonAccountId = x.CreditPersonAccountId,
                                         Total = x.Total,
                                         DebtPersonAccountTypeId = x.DebtPersonAccountTypeId,
                                         CreditPersonAccountTypeId = x.CreditPersonAccountTypeId,
                                         Notes = x.Notes
                                     }).ToList();
            }

            if (ficheDto.ProductList != null && ficheDto.ProductList.Count > 0)
            {
                // ficheDto.ProductList.Count()
                foreach (var line in ficheDto.ProductList.Where(w => !w.IsDeleted).GroupBy(info => info.ProductId)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Metric))
                {

                    if (line.Count > 1)
                    {
                        responseDto.IsSuccess = false;
                        responseDto.Message = "Birden fazla aynı ürün var hacı.";
                        return responseDto;
                    }
                }


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

                                           // Business will calculate below values:
                                           // Total
                                           // Discount total
                                           // VatTotal
                                           // GrandTotal
                                       }).ToList();
            }

            if (ficheDto.RelationList != null && ficheDto.RelationList.Count > 0)
            {
                ficheBo.RelationList = (from x in ficheDto.RelationList
                                        select new FicheRelationSaveBo
                                        {
                                            ChildFicheId = x.ChildFicheId,
                                            FicheRelationTypeId = x.FicheRelationTypeId
                                        }).ToList();
            }


            // No need to pass the 'VatTotalList'. Because business will calculate it.

            ResponseBo responseBo = ficheBusiness.Save(ficheBo);

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            responseDto = responseBo.ToResponseDto();
            return responseDto;
        }


        [HttpPost]
        public ResponseDto Delete(FicheDeleteDto deleteDto)
        {
            ResponseDto responseDto = new ResponseDto();

            FicheDeleteBo deleteBo = new FicheDeleteBo()
            {
                FicheId = deleteDto.FicheId,

                Session = Session
            };

            ResponseBo responseBo = ficheBusiness.Delete(deleteBo);
            responseDto = responseBo.ToResponseDto();

            base.SendNotifyWsToList(responseBo.PersonNotifyList);

            return responseDto;
        }


        [HttpPost]
        public ResponseDto<List<FicheApprovalHistoryListDto>> GetApprovalHistoryList(FicheApprovalHistoryGetListCriteriaDto criteriaDto)
        {
            FicheApprovalHistoryGetListCriteriaBo criteriaBo = new FicheApprovalHistoryGetListCriteriaBo()
            {
                FicheId = criteriaDto.FicheId,

                Session = Session
            };

            ResponseBo<List<FicheApprovalHistoryListBo>> responseBo = ficheBusiness.GetApprovalHistoryList(criteriaBo);

            ResponseDto<List<FicheApprovalHistoryListDto>> responseDto = responseBo.ToResponseDto<List<FicheApprovalHistoryListDto>, List<FicheApprovalHistoryListBo>>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new List<FicheApprovalHistoryListDto>();
                foreach (FicheApprovalHistoryListBo itemBo in responseBo.Bo)
                {
                    responseDto.Dto.Add(new FicheApprovalHistoryListDto()
                    {
                        Id = itemBo.Id,
                        ApprovalStatId = itemBo.ApprovalStatId,
                        PersonId = itemBo.PersonId,
                        PersonFullName = itemBo.PersonFullName,
                        CreateDateNumber = itemBo.CreateDate.ToNumberFromDateTime()
                    });
                }
            }

            return responseDto;
        }


        [HttpPost]
        public ResponseDto GetNextId()
        {
            return ficheBusiness.GetNextId().ToResponseDto();
        }

        [HttpPost]
        public ResponseDto<FicheActionsDto> GetActions(FicheGetActionsCriteriaDto criteriaDto)
        {
            FicheGetActionsCriteriaBo criteriaBo = new FicheGetActionsCriteriaBo()
            {
                FicheId = criteriaDto.FicheId,

                Session = Session
            };

            ResponseBo<FicheActionsBo> responseBo = ficheBusiness.GetActions(criteriaBo);

            ResponseDto<FicheActionsDto> responseDto = responseBo.ToResponseDto<FicheActionsDto, FicheActionsBo>();

            if (responseBo.IsSuccess && responseBo.Bo != null)
            {
                responseDto.Dto = new FicheActionsDto()
                {
                    Deletable = responseBo.Bo.Deletable,
                    Acceptable = responseBo.Bo.Acceptable,
                    Rejectable = responseBo.Bo.Rejectable,
                    PullBackable = responseBo.Bo.PullBackable,

                    Commentable = responseBo.Bo.Commentable
                };
            }

            return responseDto;
        }
    }
}