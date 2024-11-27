using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Company;
using api.Dtos.Stock;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
        Task<List<SearchStockRequestDto>> SearchCompaniesAsync(string query);
        Task<CompanyProfileDto> GetCompanyProfileAsync(string ticker);
        Task<CompanyKeyMetricsDto> GetCompanyKeyMetricsAsync(string query);
        Task<CompanyIncomeStatementDto> GetCompanyIncomeStatementAsync(string query);
        Task<CompanyBalanceSheetDto> GetCompanyBalanceSheetAsync(string query);
        Task<CompanyCashFlowDto> GetCompanyCashFlowAsync(string query);
        Task<CompanyTenKDto> GetCompanyTenKAsync(string query);
        Task<CompanyHistoricalDividendDto> GetCompanyHistoricalDividendAsync(string query);
    }
}