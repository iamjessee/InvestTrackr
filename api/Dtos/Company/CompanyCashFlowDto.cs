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
        public int deferredIncomeTax { get; set; }
        public long stockBasedCompensation { get; set; }
        public long changeInWorkingCapital { get; set; }
        public int accountsReceivables { get; set; }
        public int inventory { get; set; }
        public long accountsPayables { get; set; }
        public int otherWorkingCapital { get; set; }
        public int otherNonCashItems { get; set; }
        public long netCashProvidedByOperatingActivities { get; set; }
        public int investmentsInPropertyPlantAndEquipment { get; set; }
        public int acquisitionsNet { get; set; }
        public long purchasesOfInvestments { get; set; }
        public long salesMaturitiesOfInvestments { get; set; }
        public int otherInvestingActivites { get; set; }
        public int netCashUsedForInvestingActivites { get; set; }
        public int debtRepayment { get; set; }
        public int commonStockIssued { get; set; }
        public long commonStockRepurchased { get; set; }
        public int dividendsPaid { get; set; }
        public long otherFinancingActivites { get; set; }
        public long netCashUsedProvidedByFinancingActivities { get; set; }
        public int effectOfForexChangesOnCash { get; set; }
        public long netChangeInCash { get; set; }
        public long cashAtEndOfPeriod { get; set; }
        public long cashAtBeginningOfPeriod { get; set; }
        public long operatingCashFlow { get; set; }
        public int capitalExpenditure { get; set; }
        public long freeCashFlow { get; set; }
        public string link { get; set; }
        public string finalLink { get; set; }
    }
}