using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager; 
        }

        // Registers a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    return BadRequest(ModelState); // Return validation errors
                }

                var appUser = new AppUser 
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                // Create the user and check if the creation succeeded
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    // Add user to the "User" role
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    return roleResult.Succeeded ? Ok("User Created") : BadRequest(roleResult.Errors);
                }
                
                // Handle user creation failure
                return StatusCode(StatusCodes.Status500InternalServerError, createdUser.Errors); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex); // Handle unexpected errors
            }
        }
    }
}