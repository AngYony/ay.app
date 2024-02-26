using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WY.Common.Token
{
    public static class TokenHelper
    {
        public static string CreateToken(JwtTokenOptions tokenOptions, string customerNo, string customerName)
        {
            // 准备令牌的声明
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, customerName),
                new Claim("CustomerNo",customerNo)
            };

            // 生成签名证书
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 生成令牌
            var jwtToken = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: DateTime.Now.AddMinutes(tokenOptions.AccessExpiration),
                signingCredentials: credentials,
                claims: claims);

            // 序列化令牌
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
