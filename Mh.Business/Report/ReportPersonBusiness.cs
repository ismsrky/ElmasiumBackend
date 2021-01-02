using Dapper;
using Mh.Business.Bo.Report;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Report;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Mh.Business.Report
{
    public class ReportPersonBusiness : BaseBusiness, IReportPersonBusiness
    {
        public ResponseBo<ReportPersonSummaryBo> GetSummary(ReportPersonSummaryGetCriteriaBo criteriaBo)
        {
            ResponseBo<ReportPersonSummaryBo> responseBo = new ResponseBo<ReportPersonSummaryBo>();
            try
            {
                using (SqlConnection conn = DbAccess.Connection.GetConn())
                {
                    var p = new DynamicParameters();
                    p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    p.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    p.Add("@SaleGrandTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);
                    p.Add("@SaleCostTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);

                    p.Add("@CreditTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);
                    p.Add("@DebtTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);

                    p.Add("@RevenueCashTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);
                    p.Add("@RevenueBankTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);
                    p.Add("@ChargeSaleTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);

                    p.Add("@PersonAccountCashTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);
                    p.Add("@PersonAccountBankTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);
                    p.Add("@PersonAccountCreditCardTotal", dbType: DbType.Decimal, direction: ParameterDirection.Output, precision: 18, scale: 2);

                    p.Add("@IssueDateStart", criteriaBo.IssueDateStart.Date, DbType.Date, ParameterDirection.Input);
                    p.Add("@IssueDateEnd", criteriaBo.IssueDateEnd.Date, DbType.Date, ParameterDirection.Input);
                    p.Add("@CurrencyId", criteriaBo.CurrencyId, DbType.Int32, ParameterDirection.Input);

                    p.Add("@MyPersonId", criteriaBo.Session.MyPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@OperatorRealId", criteriaBo.Session.RealPerson.Id, DbType.Int64, ParameterDirection.Input);
                    p.Add("@LanguageId", criteriaBo.Session.RealPerson.LanguageId, DbType.Int32, ParameterDirection.Input);

                    conn.Execute("spReportPersonSummary", p, commandType: CommandType.StoredProcedure);
                    responseBo.Message = p.Get<string>("@Message");
                    responseBo.IsSuccess = p.Get<bool>("@IsSuccess");

                    responseBo.Bo = new ReportPersonSummaryBo();
                    responseBo.Bo.SaleGrandTotal = p.Get<decimal>("@SaleGrandTotal");
                    responseBo.Bo.SaleCostTotal = p.Get<decimal>("@SaleCostTotal");

                    responseBo.Bo.CreditTotal = p.Get<decimal>("@CreditTotal");
                    responseBo.Bo.DebtTotal = p.Get<decimal>("@DebtTotal");

                    responseBo.Bo.RevenueCashTotal = p.Get<decimal>("@RevenueCashTotal");
                    responseBo.Bo.RevenueBankTotal = p.Get<decimal>("@RevenueBankTotal");
                    responseBo.Bo.ChargeSaleTotal = p.Get<decimal>("@ChargeSaleTotal");

                    responseBo.Bo.PersonAccountCashTotal = p.Get<decimal>("@PersonAccountCashTotal");
                    responseBo.Bo.PersonAccountBankTotal = p.Get<decimal>("@PersonAccountBankTotal");
                    responseBo.Bo.PersonAccountCreditCardTotal = p.Get<decimal>("@PersonAccountCreditCardTotal");
                }
            }
            catch (Exception ex)
            {
                responseBo = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name, criteriaBo).ToResponse<ReportPersonSummaryBo>();
            }

            return responseBo;
        }
    }
}