using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class SearchStockRequestDto
    {

    [Required]
    public string Symbol { get; set; }

    [Required]
    public string Name { get; set; }

    public string Currency { get; set; }
    public string ExchangeShortName { get; set; }
    public string StockExchange { get; set; }
    }
}