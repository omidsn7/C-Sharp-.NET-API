using APITokenDemo.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APITokenDemo.JWT
{
    public class IAuth : IJwtAuth
    {
        private readonly string key;

        public IAuth(string key)
        {
            this.key = key;
        }

        public string Authentication(UserModel user)
        {
            //create security token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //create private ket to encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Email, user.EmailAddress),
                        new Claim(ClaimTypes.GivenName, user.GivenName),
                        new Claim(ClaimTypes.Surname, user.Surname),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            //create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);


            //return token From method
            return tokenHandler.WriteToken(token);

        }
    }
}
