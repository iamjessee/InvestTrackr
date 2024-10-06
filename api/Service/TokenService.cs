using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        // Constructor to inject configuration and create a security key from the JWT signing key
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])); // Load signing key from config
        }
        
        // Creates a JWT token for the authenticated AppUser
        public string CreateToken(AppUser appUser)
        {
            // Define the claims that will be included in the JWT payload
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email), // Claim for user's email
                new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName) // Claim for username
            };

            // Create signing credentials using HMAC-SHA512 and the security key
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Define the JWT token settings (subject, expiration, signing credentials, issuer, audience)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Attach claims to the token
                Expires = DateTime.Now.AddDays(7), // Set token expiration (7 days)
                SigningCredentials = credentials, // Add signing credentials
                Issuer = _config["JWT:Issuer"], // Specify the token issuer
                Audience = _config["JWT:Audience"] // Specify the token audience
            };

            // Create the token handler and generate the JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); // Create the token based on the descriptor

            // Return the serialized token as a string
            return tokenHandler.WriteToken(token);
        }
    }
}