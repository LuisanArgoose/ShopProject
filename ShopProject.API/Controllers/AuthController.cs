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

        [HttpGet]
        public IActionResult RequestToken(string login, string password)
        {
            var tokenLogin = _context.TokenLogins.Where(x => x.Login == login);
            if(!tokenLogin.Any())
            {
                return BadRequest("Неправильные учетные данные");
            }
            if(tokenLogin.First().Password != password) 
            {
                return BadRequest("Неправильные учетные данные");
            }


            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login)
            };
            

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
            });
            
        }
    }

}
