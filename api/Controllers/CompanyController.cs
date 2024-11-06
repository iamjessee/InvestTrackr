using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/company")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly IFMPService _fmpService;

        public CompanyController(IFMPService fmpService)
        {
            _fmpService = fmpService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Stock>>> SearchCompanies([FromQuery] string query)
        {
            var companies = await _fmpService.SearchCompaniesAsync(query);

            if(companies == null || companies.Count == 0)
            {
                return NotFound($"No company data found for {query}");
            }

            return Ok(companies);
        }

        [HttpGet("companyprofile")]
        public async Task<IActionResult> GetCompanyProfile(string ticker)
        {
            var companyProfile = await _fmpService.GetCompanyProfileAsync(ticker);

            if(companyProfile == null)
            {
                return NotFound("Error retrieving company profile data");
            }

            return Ok(companyProfile);
        }

        [HttpGet("companycompdata/{symbol}")]
        public async Task<IActionResult> GetCompData(string symbol)
        {
            var compData = await _fmpService.GetCompData(symbol);

            if(compData == null)
            {
                return NotFound($"Error retreving comp data for {symbol}");
            }

            return Ok(compData);
        }
    }
}