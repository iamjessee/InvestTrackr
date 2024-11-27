using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyTenKDto
    {
        public string Symbol { get; set; }
        public string FillingDate { get; set; }
        public string AcceptedDate { get; set; }
        public string Cik { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string FinalLink { get; set; }
    }
}