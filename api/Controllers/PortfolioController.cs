using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/protfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortoflio(appUser);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                return BadRequest("stock not found");
            }

            var userPortfolio = await _portfolioRepository.GetUserPortoflio(appUser);

            if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("cannot add same stock to portfolio");
            }

            var portfolioModel = new Portfolios
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepository.CreateAsync(portfolioModel);

            if(portfolioModel == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error creating");
            }
            else
            {
                return Created();
            }
        }

            [HttpDelete]
            [Authorize]
            public async Task<IActionResult> DeletePortfolio(string symbol)
            {
                var userName = User.GetUsername();
                var appUser = await _userManager.FindByNameAsync(userName);

                var userPortfolio = await _portfolioRepository.GetUserPortoflio(appUser);

                var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

                if(filteredStock.Count() != 1)
                {
                    return BadRequest("stock not found in portfolio");
                    
                }
                else
                {
                    await _portfolioRepository.DeletePortfolioAsync(appUser, symbol);
                }

                return Ok("successfully removed");
            }
    }
}