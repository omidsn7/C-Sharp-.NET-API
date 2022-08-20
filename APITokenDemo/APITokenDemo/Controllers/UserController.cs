using APITokenDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APITokenDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        //private readonly IJwtAuth jwtAuth;
        //public UserController(IJwtAuth jwtAuth)
        //{
        //    this.jwtAuth = jwtAuth;
        //}

        [HttpGet("Admin")]
        //[AllowAnonymous]
        public IActionResult Admin()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, You Are An {currentUser.Role}");
        }

        [HttpGet("Admins")]
        [Authorize(Roles ="Administrator")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, You Are An {currentUser.Role}");
        }

        //[HttpGet("Admins")]
        //[Authorize(Roles ="Administrator")]
        //public IActionResult AdminsEndpoint()
        //{
        //    var currentUser = GetCurrentUser();

        //    return Ok($"Hi {currentUser.GivenName}, You Are An {currentUser.Role}");
        //}

        [HttpGet("Sellers")]
        [Authorize(Roles ="Sellers")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.GivenName}, You Are An {currentUser.Role}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi , You're On Public Property");
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }
            return null;
        }
    }
}
