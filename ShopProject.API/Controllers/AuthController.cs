using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;

namespace ShopProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ServerAPIDbContext context) : Controller
    {
        private readonly ServerAPIDbContext _context = context;
        private const string SecretKey = "MySuperSecretSecretKeyShopProjectSecretKey"; // секретный ключ для подписи токена

        private string GenerateToken(string name)
        {
            var claims = new[]
{
                new Claim(ClaimTypes.Name, "your_username"),
                new Claim(ClaimTypes.Role, "your_role")
};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "your_issuer",
                "your_audience",
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


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


            var tokenString = GenerateToken(login);

            return Ok(new { Token = tokenString });


        }
    }

}
