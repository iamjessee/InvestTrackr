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

        // Searches FMP stock api for companies that match search query
        [HttpGet("search")]
        public async Task<ActionResult<List<Stock>>> SearchCompanies([FromQuery] string query)
        {
            var companies = await _fmpService.SearchCompaniesAsync(query);

            if (companies == null || companies.Count == 0)
            {
                return NotFound($"No company data found for {query}");
            }

            return Ok(companies);
        }

        // Retrevie selected company profile data based on searched and selected ticker
        [HttpGet("companyprofile")]
        public async Task<IActionResult> GetCompanyProfile(string ticker)
        {
            var companyProfile = await _fmpService.GetCompanyProfileAsync(ticker);

            if (companyProfile == null)
            {
                return NotFound("Error retrieving company profile data");
            }

            return Ok(companyProfile);
        }

        // Rereive keymetrics data based on selected company/ticker/symbol
        [HttpGet("keymetrics")]
        public async Task<IActionResult> GetCompanyKeyMetrics([FromQuery] string query)
        {
            var KeyMetrics = await _fmpService.GetCompanyKeyMetricsAsync(query);

            if (KeyMetrics == null)
            {
                return NotFound("Error retrieving company keymetric data");
            }

            return Ok(KeyMetrics);
        }
    }
}