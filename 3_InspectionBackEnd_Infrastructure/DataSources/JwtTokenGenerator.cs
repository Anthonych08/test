using _1_InspectionBackEnd_Domain.Master;
using _2_InspectionBackEnd_Application.Interfaces;
using _3_InspectionBackEnd_Infrastructure.Settings;
using Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _3_InspectionBackEnd_Infrastructure.DataSources
{
    public class JwtTokenGenerator : IJwt
    {
        private readonly Infrastructure_Setting _infrastructureSettings;

        public JwtTokenGenerator(
            IOptions<Infrastructure_Setting> infrastructureSettings
            )
        {
            _infrastructureSettings = infrastructureSettings.Value;
        }

        public string GenerateToken (MasterUser user, string roleName)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.EMAIL),
                new Claim(ClaimTypes.Role, roleName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_infrastructureSettings.Jwt.Key));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
