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
    }
}