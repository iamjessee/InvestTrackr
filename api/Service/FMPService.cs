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
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public FMPService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        
      public async Task<Stock> FindStockBySymbolAsync(string symbol)
{
    try
    {
        var apikey = _configuration["FMPKey"];
        var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={apikey}");

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);

            if (tasks != null && tasks.Length > 0)
            {
                var stock = tasks[0];
                return stock?.ToStockFromFMPStock();
            }
            else
            {
                Console.WriteLine($"No stock data found for symbol: {symbol}");
                return null;
            }
        }
        else
        {
            Console.WriteLine($"API call failed with status code: {result.StatusCode}");
            return null;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching stock data: {ex.Message}");
        Console.WriteLine(ex.StackTrace);  // Log stack trace for debugging
        return null;
    }
}
}
}