using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyIncomeStatementDto
    {
        public string Date { get; set; }
        public string Symbol { get; set; }
        public string ReportedCurrency { get; set; }
        public string Cik { get; set; }
        public string FillingDate { get; set; }
        public string AcceptedDate { get; set; }
        public string CalendarYear { get; set; }
        public string Period { get; set; }
        public decimal Revenue { get; set; }
        public decimal CostOfRevenue { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal GrossProfitRatio { get; set; }
        public decimal ResearchAndDevelopmentExpenses { get; set; }
        public decimal GeneralAndAdministrativeExpenses { get; set; }
        public decimal SellingAndMarketingExpenses { get; set; }
        public decimal SellingGeneralAndAdministrativeExpenses { get; set; }
        public decimal OtherExpenses { get; set; }
        public decimal OperatingExpenses { get; set; }
        public decimal CostAndExpenses { get; set; }
        public decimal InterestIncome { get; set; }
        public decimal InterestExpense { get; set; }
        public decimal DepreciationAndAmortization { get; set; }
        public decimal Ebitda { get; set; }
        public decimal EbitdaRatio { get; set; }
        public decimal OperatingIncome { get; set; }
        public decimal OperatingIncomeRatio { get; set; }
        public decimal TotalOtherIncomeExpensesNet { get; set; }
        public decimal IncomeBeforeTax { get; set; }
        public decimal IncomeBeforeTaxRatio { get; set; }
        public decimal IncomeTaxExpense { get; set; }
        public decimal NetIncome { get; set; }
        public decimal NetIncomeRatio { get; set; }
        public decimal Eps { get; set; }
        public decimal EpsDiluted { get; set; }
        public decimal WeightedAverageShsOut { get; set; }
        public decimal WeightedAverageShsOutDil { get; set; }
        public string Link { get; set; }
        public string FinalLink { get; set; }
    }
}