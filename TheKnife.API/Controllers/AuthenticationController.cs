using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheKnife.API.Models.Authentication;
using TheKnife.API.Models.Settings;

namespace TheKnife.API.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly JwtSettings _jwtSettings;

        public AuthenticationController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody, Required] LoginRequest loginRequest)
        {
            if (loginRequest.UserName == "RuiBarreto" && loginRequest.Password == "123RuiBarreto456")
            {
                var issuer = _jwtSettings.Issuer;
                var audience = _jwtSettings.Audience;
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loginRequest.UserName),
                new Claim(JwtRegisteredClaimNames.Email, "a@a.pt"),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);

                return Ok(new LoginResponse(stringToken));
            }
            return Unauthorized();
        }
    }
}
