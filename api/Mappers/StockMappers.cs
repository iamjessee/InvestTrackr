using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        // Maps a Stock model to a Stock DTO for data transfer
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                // Maps the comments associated with the stock to their DTOs
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        // Converts a CreateStockRequest DTO to a Stock model for creation
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

        public static Stock ToStockFromFMPStock(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap
            };
        }
    }
}