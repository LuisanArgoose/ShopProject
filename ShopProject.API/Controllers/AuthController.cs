using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ServerAPIDbContext context) : Controller
    {
        private readonly ServerAPIDbContext _context = context;
        private const string SecretKey = "mysupersecret_secretkey!123"; // секретный ключ для подписи токена
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        [HttpPost]
        public IActionResult RequestToken([FromBody] LoginViewModel login)
        {
            _context.Workers.Select(x => new { x.Login, x.Password });
            if (login.Username == "user" && login.Password == "password")
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, login.Username)
            };

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return BadRequest("Неправильные учетные данные");
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
