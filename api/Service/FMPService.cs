using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
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
    }
}