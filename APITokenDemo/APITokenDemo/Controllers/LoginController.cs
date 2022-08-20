using APITokenDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace APITokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IJwtAuth jwtAuth;

        public LoginController(IConfiguration config , IJwtAuth jwtAuth)
        {
            _config = config;
            this.jwtAuth = jwtAuth;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
                //var token = jwtAuth.Authentication(userLogin.Username, userLogin.Password);
                //return Ok(token);
            }
            else
            {
                return NotFound("User not found");
            }
        }

        private string Generate(UserModel user)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = jwtAuth.Authentication(user);


            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Username),
            //    new Claim(ClaimTypes.Email, user.EmailAddress),
            //    new Claim(ClaimTypes.GivenName, user.GivenName),
            //    new Claim(ClaimTypes.Surname, user.Surname),
            //    new Claim(ClaimTypes.Role, user.Role)
            //};                              

            return token;
            //var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
            //    claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);

            //return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(o => o.Username.ToLower() ==
                userLogin.Username.ToLower() && o.Password == userLogin.Password);

            if (currentUser !=null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
