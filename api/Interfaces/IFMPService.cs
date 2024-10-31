using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Company;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
        Task<List<SearchStockRequestDto>> SearchCompaniesAsync(string query);
        Task<CompanyProfileDto> GetCompanyProfileAsync(string ticker);
    }
}