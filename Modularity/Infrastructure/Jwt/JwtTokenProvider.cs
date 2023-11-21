using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Jwt
{
    public class JwtTokenProvider
    {
        private static readonly JwtOption _options;

        private static readonly JwtSecurityTokenHandler _tokenHandler;

        private static readonly TokenValidationParameters _validationParameters;

        static JwtTokenProvider()
        {
            _options = JwtOption.GetDefaultOptions();
            _validationParameters = JwtOption.GetDefaultTokenValidationParameters(_options);
            _tokenHandler = new JwtSecurityTokenHandler();
        }


      
        private static string Generate(JwtUser user, int expMins)
        {
          
            var claims = new[] {
                new Claim(ClaimsModel.Id,user.Id.ToString())
            };
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
          
            JwtSecurityToken security = new(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expMins),
                signingCredentials: creds);
           
            var token = _tokenHandler.WriteToken(security);

            return token;
        }



        public static string GenerateToken(long id,int expMins = 60)
        {
            JwtUser user = new()
            {
                Id = id,               
            };
            return Generate(user, expMins);
        }

        public static DateTime GetExpirationTime(string token, bool getLocalTime = true)
        {
            try
            {
                JwtSecurityToken jwt = _tokenHandler.ReadJwtToken(token);

                return getLocalTime ? TimeZoneInfo.ConvertTimeFromUtc(jwt.ValidTo, TimeZoneInfo.Local)
                                   : jwt.ValidTo;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static bool TryGetExpirationTime(string jwtToken, out DateTime expirationTime, bool getLocalTime = true)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = _tokenHandler.ValidateToken(jwtToken, _validationParameters, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                {
                    throw new Exception("JwtSecurityToken is Null");
                }

                var time = jwtSecurityToken!.ValidTo; // 获取过期时间

                expirationTime = getLocalTime ? TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.Local)
                                   : time;

                return true;
            }
            catch (Exception)
            {
                expirationTime = DateTime.MinValue;
                return false;
            }
        }

        public static bool TryGetExpirationTimestamp(string jwtToken, out long? exp)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = _tokenHandler.ValidateToken(jwtToken, _validationParameters, out SecurityToken validatedToken);
              
                if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                {
                    throw new Exception("JwtSecurityToken is Null");
                }

                exp = jwtSecurityToken!.Payload.Expiration;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                exp = 0;
                return false;

            }
        }

    
        public static bool TryGetClaim(string jwtToken,out long? userId)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = _tokenHandler.ValidateToken(jwtToken, _validationParameters, out SecurityToken validatedToken);

                if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                {
                    throw new Exception("JwtSecurityToken is Null");
                }

                var claims = jwtSecurityToken?.Claims;

                var _userId = claims?.FirstOrDefault(t => t.Type == "id")?.Value;

                if (string.IsNullOrWhiteSpace(_userId))
                {
                    userId = 0;
                }
                else
                {
                    userId = long.Parse(_userId);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex is not SecurityTokenExpiredException) 
                {
                    Console.WriteLine(ex);
                }             
                userId = 0;
                return false;
            }
        }
    }
}
