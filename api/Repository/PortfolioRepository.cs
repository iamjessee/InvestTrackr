using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        // Adds new portfolio to the database
        public async Task<Portfolios> CreateAsync(Portfolios portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        // Deletes portfolio entry based on authorized user and stock symbol
        public async Task<Portfolios> DeletePortfolioAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if(portfolioModel == null)
            {
                return null; // Return null if portfolio entry is not found
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            
            return portfolioModel;
        }

        // Retrives portfolio based on authorized user
        public async Task<List<Stock>> GetUserPortoflio(AppUser appUser)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == appUser.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}