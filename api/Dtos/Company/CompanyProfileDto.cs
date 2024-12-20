using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Company
{
    public class CompanyProfileDto
    {
    public string Symbol { get; set; }
    public double Price { get; set; }
    public double Beta { get; set; }
    public int VolAvg { get; set; }
    public long MktCap { get; set; }
    public decimal LastDiv { get; set; }
    public string Range { get; set; }
    public double Changes { get; set; }
    public string CompanyName { get; set; }
    public string Currency { get; set; }
    public string Cik { get; set; }
    public string Isin { get; set; }
    public string Exchange { get; set; }
    public string ExchangeShortName { get; set; }
    public string Industry { get; set; }
    public string Website { get; set; }
    public string Description { get; set; }
    public string Ceo { get; set; }
    public string Sector { get; set; }
    public string Counter { get; set; }
    public string FullTimeEmployees { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public double DcfDiff { get; set; }
    public double Dcf { get; set; }
    public string Image { get; set; }
    public string IpoDate { get; set; }
    public bool DefaultImage { get; set; }
    public bool IsEtf { get; set; }
    public bool IsActivelyTrading { get; set; }
    public bool IsAdr { get; set; }
    public bool IsFund { get; set; }
    }
}