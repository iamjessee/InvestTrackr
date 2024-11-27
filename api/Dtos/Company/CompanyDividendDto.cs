using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyDividendDto
    {
        public string date { get; set; }
        public string label { get; set; }
        public double adjDividend { get; set; }
        public double dividend { get; set; }
        public string recordDate { get; set; }
        public string paymentDate { get; set; }
        public string declarationDate { get; set; }
    }
}