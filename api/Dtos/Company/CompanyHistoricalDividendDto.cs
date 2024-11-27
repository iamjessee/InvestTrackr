using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyHistoricalDividendDto
    {
    public string Symbol { get; set; }
    public List<CompanyDividendDto> historical { get; set; }
    }
}