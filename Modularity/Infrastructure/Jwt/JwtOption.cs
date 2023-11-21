using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtOption
    {
      
        public required string  Audience { get; set; }

    
        public required string SecurityKey { get; set; }
      
        public  required string Issuer { get; set; }


        /// <summary>
        /// 获取默认的Options
        /// </summary>
        /// <returns></returns>
        public static JwtOption GetDefaultOptions()
        {
            return new JwtOption
            {
                Audience = "Customer",
                Issuer = "DiamondHusky",
                SecurityKey = "VIXDzXgr8Bao8Ae8vs9y4gryNiWM8RC2O1i8yvUYCgRI7rHa7xJZqa9bzYFwog5x1iQ7l3L0YxaYSc7AluLR",
            };
        }

        public static TokenValidationParameters GetDefaultTokenValidationParameters(JwtOption? option = null)
        {
            option ??= GetDefaultOptions();

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = option.Issuer,
                ValidAudience = option.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.SecurityKey)),
                ClockSkew = TimeSpan.FromSeconds(10)
            };
        }
    }
}
