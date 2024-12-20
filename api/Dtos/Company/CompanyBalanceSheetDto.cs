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
        public long CashAndCashEquivalents { get; set; }
        public long ShortTermInvestments { get; set; }
        public long CashAndShortTermInvestments { get; set; }
        public long NetReceivables { get; set; }
        public long Inventory { get; set; }
        public long OtherCurrentAssets { get; set; }
        public long TotalCurrentAssets { get; set; }
        public long PropertyPlantEquipmentNet { get; set; }
        public long Goodwill { get; set; }
        public long IntangibleAssets { get; set; }
        public long GoodwillAndIntangibleAssets { get; set; }
        public long LongTermInvestments { get; set; }
        public int TaxAssets { get; set; }
        public long OtherNonCurrentAssets { get; set; }
        public long TotalNonCurrentAssets { get; set; }
        public int OtherAssets { get; set; }
        public long TotalAssets { get; set; }
        public long AccountPayables { get; set; }
        public long ShortTermDebt { get; set; }
        public int TaxPayables { get; set; }
        public long DeferredRevenue { get; set; }
        public long OtherCurrentLiabilities { get; set; }
        public long TotalCurrentLiabilities { get; set; }
        public long LongTermDebt { get; set; }
        public int DeferredRevenueNonCurrent { get; set; }
        public int DeferredTaxLiabilitiesNonCurrent { get; set; }
        public long OtherNonCurrentLiabilities { get; set; }
        public long TotalNonCurrentLiabilities { get; set; }
        public int OtherLiabilities { get; set; }
        public int CapitalLeaseObligations { get; set; }
        public long TotalLiabilities { get; set; }
        public int PreferredStock { get; set; }
        public long CommonStock { get; set; }
        public long RetainedEarnings { get; set; }
        public int AccumulatedOtherComprehensiveIncomeLoss { get; set; }
        public long OthertotalStockholdersEquity { get; set; }
        public long TotalStockholdersEquity { get; set; }
        public long TotalEquity { get; set; }
        public long TotalLiabilitiesAndStockholdersEquity { get; set; }
        public int MinorityInterest { get; set; }
        public long TotalLiabilitiesAndTotalEquity { get; set; }
        public long TotalInvestments { get; set; }
        public long TotalDebt { get; set; }
        public long NetDebt { get; set; }
        public string Link { get; set; }
        public string FinalLink { get; set; }
    }
}