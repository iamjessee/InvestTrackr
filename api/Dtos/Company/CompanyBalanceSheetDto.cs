using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyBalanceSheetDto
    {
    public string Date { get; set; }
    public string Symbol { get; set; }
    public string ReportedCurrency { get; set; }
    public string Cik { get; set; }
    public string FillingDate { get; set; }
    public string AcceptedDate { get; set; }
    public string CalendarYear { get; set; }
    public string Period { get; set; }
    public decimal CashAndCashEquivalents { get; set; }
    public decimal ShortTermInvestments { get; set; }
    public decimal CashAndShortTermInvestments { get; set; }
    public decimal NetReceivables { get; set; }
    public decimal Inventory { get; set; }
    public decimal OtherCurrentAssets { get; set; }
    public decimal TotalCurrentAssets { get; set; }
    public decimal PropertyPlantEquipmentNet { get; set; }
    public decimal Goodwill { get; set; }
    public decimal IntangibleAssets { get; set; }
    public decimal GoodwillAndIntangibleAssets { get; set; }
    public decimal LongTermInvestments { get; set; }
    public decimal TaxAssets { get; set; }
    public decimal OtherNonCurrentAssets { get; set; }
    public decimal TotalNonCurrentAssets { get; set; }
    public decimal OtherAssets { get; set; }
    public decimal TotalAssets { get; set; }
    public decimal AccountPayables { get; set; }
    public decimal ShortTermDebt { get; set; }
    public decimal TaxPayables { get; set; }
    public decimal DeferredRevenue { get; set; }
    public decimal OtherCurrentLiabilities { get; set; }
    public decimal TotalCurrentLiabilities { get; set; }
    public decimal LongTermDebt { get; set; }
    public decimal DeferredRevenueNonCurrent { get; set; }
    public decimal DeferredTaxLiabilitiesNonCurrent { get; set; }
    public decimal OtherNonCurrentLiabilities { get; set; }
    public decimal TotalNonCurrentLiabilities { get; set; }
    public decimal OtherLiabilities { get; set; }
    public decimal CapitalLeaseObligations { get; set; }
    public decimal TotalLiabilities { get; set; }
    public decimal PreferredStock { get; set; }
    public decimal CommonStock { get; set; }
    public decimal RetainedEarnings { get; set; }
    public decimal AccumulatedOtherComprehensiveIncomeLoss { get; set; }
    public decimal OtherTotalStockholdersEquity { get; set; }
    public decimal TotalStockholdersEquity { get; set; }
    public decimal TotalEquity { get; set; }
    public decimal TotalLiabilitiesAndStockholdersEquity { get; set; }
    public decimal MinorityInterest { get; set; }
    public decimal TotalLiabilitiesAndTotalEquity { get; set; }
    public decimal TotalInvestments { get; set; }
    public decimal TotalDebt { get; set; }
    public decimal NetDebt { get; set; }
    public string Link { get; set; }
    public string FinalLink { get; set; }
    }
}