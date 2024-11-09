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
        /// <returns>A Task that represents the asynchronous operation, containing the stock information if found.</returns>
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
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
                        return tasks[0].ToStockFromFMPStock();
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

        public async Task<CompanyProfileDto> GetCompanyProfileAsync(string ticker)
        {
            try
            {
                var apikey = _configuration["FMPKey"];

                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{ticker}?apikey={apikey}");

                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);

                    if(tasks != null && tasks.Length > 0)
                    {
                        return tasks[0].ToCompanyProfileDto();
                    }
                }

                Console.WriteLine($"API call failed with status code: {result.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the API call
                Console.WriteLine($"Error fetching company profile data: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Asynchronously retrieves stock information by search string.
        /// </summary>
        /// <param name="query">Search term inserted by user</param>
        /// <returns>List of matching companies related to search term if found</returns>
        public async Task<List<SearchStockRequestDto>> SearchCompaniesAsync(string query)
        {
            try
            {
                var apikey = _configuration["FMPKey"];
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/search?query={query}&limit=10&exchange=NASDAQ&apikey={apikey}");

                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var task = JsonConvert.DeserializeObject<SearchStockRequestDto[]>(content);

                    if(task != null && task.Length > 0)
                    {
                        return task.ToList();
                    }

                    Console.WriteLine($"No results found for {query}");
                    return new List<SearchStockRequestDto>();
                }

                // Log the status code if the API call failed
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