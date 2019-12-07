using LibidoMusic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibidoMusic.Negocios
{
    public class Token
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public Token(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public UserToken BuildToken(UserInfo userInfo, Task<IList<string>> role)
        {
            IdentityOptions _options = new IdentityOptions();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(_options.ClaimsIdentity.RoleClaimType, role.Result.FirstOrDefault()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var user = _userManager.FindByNameAsync(userInfo.Email);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = userInfo.Email,
                Expiration = expiration,
                User_Id = user.Id,
                Role = role.Result.FirstOrDefault()
            };
        }
    }
}
