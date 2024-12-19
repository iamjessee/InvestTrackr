using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyCashFlowDto
    {
        public string date { get; set; }
        public string symbol { get; set; }
        public string reportedCurrency { get; set; }
        public string cik { get; set; }
        public string fillingDate { get; set; }
        public string acceptedDate { get; set; }
        public string calendarYear { get; set; }
        public string period { get; set; }
        public long netIncome { get; set; }
        public long depreciationAndAmortization { get; set; }
        public long deferredIncomeTax { get; set; }
        public long stockBasedCompensation { get; set; }
        public long changeInWorkingCapital { get; set; }
        public long accountsReceivables { get; set; }
        public long inventory { get; set; }
        public long accountsPayables { get; set; }
        public long otherWorkingCapital { get; set; }
        public long otherNonCashItems { get; set; }
        public long netCashProvidedByOperatingActivities { get; set; }
        public long investmentsInPropertyPlantAndEquipment { get; set; }
        public int acquisitionsNet { get; set; }
        public long purchasesOfInvestments { get; set; }
        public long salesMaturitiesOfInvestments { get; set; }
        public int otherInvestingActivites { get; set; }
        public long netCashUsedForInvestingActivites { get; set; }
        public long debtRepayment { get; set; }
        public int commonStockIssued { get; set; }
        public long commonStockRepurchased { get; set; }
        public long dividendsPaid { get; set; }
        public long otherFinancingActivites { get; set; }
        public long netCashUsedProvidedByFinancingActivities { get; set; }
        public int effectOfForexChangesOnCash { get; set; }
        public long netChangeInCash { get; set; }
        public long cashAtEndOfPeriod { get; set; }
        public long cashAtBeginningOfPeriod { get; set; }
        public long operatingCashFlow { get; set; }
        public long capitalExpenditure { get; set; }
        public long freeCashFlow { get; set; }
        public string link { get; set; }
        public string finalLink { get; set; }
    }
}