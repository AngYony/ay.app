using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace WY.Common.Token
{
    public static class JwtHelper
    {
        public static string CreateToken(JwtTokenOptions tokenOptions, string customerNo, string customerName, string role)
        {
            // 准备令牌的声明
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customerName),
                new Claim("CustomerNo",customerNo),
                //new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
            };

            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));


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




        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenUserModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            // token校验
            if (!string.IsNullOrEmpty(jwtStr) && jwtHandler.CanReadToken(jwtStr))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);

                object role;

                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);

                //解析值
                return new TokenUserModel { Uid = Convert.ToInt32(jwtToken.Id), Role = role == null ? "" : role.ToString() };
            }
            return null;

        }

        /// <summary>
        /// 授权解析jwt
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TokenUserModel ParsingJwtToken(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                return null;
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            TokenUserModel tm = SerializeJwt(tokenHeader);
            return tm;
        }

        #region 工良
        /// <summary>
        /// 更安全的Token生成方式
        /// </summary>
        /// <param name="tokenOptions"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string CreateToken2(JwtTokenOptions tokenOptions, string userName)
        {
            // 定义用户信息
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret));

            SecurityToken securityToken = new JwtSecurityTokenHandler().CreateToken(new SecurityTokenDescriptor
            {
                Claims = claims.ToDictionary(x => x.Type, x => (object)x.Value),
                Issuer = "http://192.168.6.6:666",
                Audience = userName,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(100),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            });
            var indf = securityToken.ToString();
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return jwtToken;
        }
        /// <summary>
        /// 检查 Token 的代码
        /// </summary>
        /// <param name="tokenOptions"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> SerializeJwt2(JwtTokenOptions tokenOptions, string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;
            if (!token.StartsWith("Bearer ")) return false;
            var newToken = token[7..];

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            if (!jwtSecurityTokenHandler.CanReadToken(newToken)) return false;

            var checkResult = await jwtSecurityTokenHandler.ValidateTokenAsync(newToken, new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret)),
            });

            if (!checkResult.IsValid) return false;

            var jwt = jwtSecurityTokenHandler.ReadJwtToken(newToken);
            IEnumerable<Claim> claims = jwt.Claims;
            return true;
        }
        #endregion

    }
    public class TokenUserModel
    {
        public int Uid { get; set; }
        public string Role { get; set; }
    }

}
