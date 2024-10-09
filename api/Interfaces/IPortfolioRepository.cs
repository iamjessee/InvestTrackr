using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>>GetUserPortoflio(AppUser appUser);
        Task<Portfolios> CreateAsync(Portfolios portfolios);
        Task<Portfolios> DeletePortfolioAsync(AppUser appUser, string symbol);
    }
}