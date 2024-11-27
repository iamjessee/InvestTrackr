using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Company;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FMPService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Asynchronously retrieves stock information by its symbol.
        /// </summary>
        /// <param name="symbol">The stock symbol to search for.</param>
        /// <returns>A Task containing the stock information if found; otherwise, null.</returns>
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                // Call the financial modeling API key
                var apikey = _configuration["FMPKey"];

                // Call the financial modeling API to get stock profile
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into an array of FMPStock
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);

                    // Check if any stock data was returned
                    if (tasks != null && tasks.Length > 0)
                    {
                        return tasks[0].ToStockFromFMPStock(); // Maps API response to domain model
                    }

                    Console.WriteLine($"No stock data found for symbol: {symbol}");
                    return null;
                }

                // Log the status code if the API call failed
                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the API call
                Console.WriteLine($"Error fetching stock data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<CompanyBalanceSheetDto> GetCompanyBalanceSheetAsync(string query)
        {
            try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/balance-sheet-statement/{query}?limit=20&apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<CompanyBalanceSheetDto[]>(content);

                    if (tasks != null && tasks.Length > 0)
                    {
                        return tasks[0];
                    }

                    Console.WriteLine($"No balance sheet found for symbol: {query}");
                    return null;
                }

                return null;
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error fetching company balance sheet data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<CompanyCashFlowDto> GetCompanyCashFlowAsync(string query)
        {
             try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/cash-flow-statement/{query}?limit=100&apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<CompanyCashFlowDto[]>(content);

                    if (tasks != null && tasks.Length > 0)
                    {
                        return tasks[0];
                    }

                    Console.WriteLine($"No cashflow statemnet found for symbol: {query}");
                    return null;
                }

                return null;
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cashflow statemnet data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<CompanyHistoricalDividendDto> GetCompanyHistoricalDividendAsync(string query)
        {
             try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/historical-price-full/stock_dividend/{query}?apikey={apikey}");

                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    Console.WriteLine(content);

                    var tasks = JsonConvert.DeserializeObject<CompanyHistoricalDividendDto>(content);

                    if(tasks != null)
                    {
                        return tasks;
                    }

                    Console.WriteLine($"No historical dividend found for {query}: {result.StatusCode}");
                    return null;
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error fetching {query} Historical Dividend: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a company's income statement data.
        /// Currently not implemented.
        /// </summary>
        /// <param name="query">The query string for the income statement.</param>
        /// <returns>A Task that represents the operation.</returns>
        public async Task<CompanyIncomeStatementDto> GetCompanyIncomeStatementAsync(string query)
        {
            try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/income-statement/{query}?limit=50&apikey={apikey}");

                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<CompanyIncomeStatementDto[]>(content);

                    if(tasks != null && tasks.Length > 0)
                    {
                        return tasks[0];
                    }

                    Console.WriteLine($"No Income Statements found for {query}: {result.StatusCode}");
                    return null;
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
             catch (Exception ex)
            {
                Console.WriteLine($"Error fetching {query} Inncome Statement: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves key metrics for a company.
        /// </summary>
        /// <param name="query">The company symbol or identifier.</param>
        /// <returns>A Task containing the key metrics data if found; otherwise, null.</returns>
        public async Task<CompanyKeyMetricsDto> GetCompanyKeyMetricsAsync(string query)
        {
            try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/key-metrics-ttm/{query}?limit=40&apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<CompanyKeyMetricsDto[]>(content);

                    if (tasks != null && tasks.Length > 0)
                    {
                        return tasks[0]; // Return the first key metrics object
                    }
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching KeyMetrics data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves the profile of a company by its ticker symbol.
        /// </summary>
        /// <param name="ticker">The company's ticker symbol.</param>
        /// <returns>A Task containing the company profile data if found; otherwise, null.</returns>
        public async Task<CompanyProfileDto> GetCompanyProfileAsync(string ticker)
        {
            try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{ticker}?apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);

                    if (tasks != null && tasks.Length > 0)
                    {
                        return tasks[0].ToCompanyProfileDto(); // Maps API response to DTO
                    }
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching company profile data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<CompanyTenKDto> GetCompanyTenKAsync(string query)
        {
             try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/sec_filings/{query}?type=10-K&page=0&apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<CompanyTenKDto[]>(content);

                    if (tasks != null && tasks.Length > 0)
                    {
                        return tasks[0];
                    }
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching company TenK data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Asynchronously searches for companies matching a given query string.
        /// </summary>
        /// <param name="query">The search term entered by the user.</param>
        /// <returns>A list of matching companies as <see cref="SearchStockRequestDto"/>.</returns>
        public async Task<List<SearchStockRequestDto>> SearchCompaniesAsync(string query)
        {
            try
            {
                var apikey = _configuration["FMPKey"];
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/search?query={query}&limit=10&exchange=NASDAQ&apikey={apikey}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var task = JsonConvert.DeserializeObject<SearchStockRequestDto[]>(content);

                    if (task != null && task.Length > 0)
                    {
                        return task.ToList(); // Convert array to a list and return
                    }

                    Console.WriteLine($"No results found for {query}");
                    return new List<SearchStockRequestDto>();
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return new List<SearchStockRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching company data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return new List<SearchStockRequestDto>();
            }
        }
    }
}