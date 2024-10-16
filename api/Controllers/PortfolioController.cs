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
        private readonly IFMPService _fmpService;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository,
        IFMPService fmpService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fmpService = fmpService;
        }

        // Get porfolio of authorized user
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortoflio(appUser);

            return Ok(userPortfolio);
        }

        //Add stock to authorized user's portfolio
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("this stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock); // add stocks if found through service
                }
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

            // Delete stock from user's portfolio
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