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
        private const string SecretKey = "MySuperSecretSecreKeyShopProjectSecretKey"; // секретный ключ для подписи токена

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


            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "ShopProject",
                audience: "ShopProject",
                claims: new[] {
                    new Claim(ClaimTypes.Role, "Terminal")
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });


        }
    }

}
