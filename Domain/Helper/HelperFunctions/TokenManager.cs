using Domain.Helper.DataObjects;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Helper.HelperFunctions
{
    public class TokenManager
    {
        private AppSetting appSetting;

        public TokenManager(IOptions<AppSetting> options)
        {
            appSetting = options.Value;
        }

        public string CreateAdminAccessToken(AdminViewModel adminViewModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", adminViewModel.AdminId.ToString()),
                    new Claim("user_email", adminViewModel.AdminUsername),
                    new Claim("user_role", "admin")
                }),
                Expires = DateTime.Now.AddMinutes(45),
                Issuer = "https://securetoken.google.com/flutter-chat-ba7c2",
                Audience = "flutter-chat-ba7c2",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string CreateUserAccessToken(UserRoleViewModel userRoleViewModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", userRoleViewModel.UserId.ToString()),
                    new Claim("user_email", userRoleViewModel.Email),
                    new Claim("user_role", userRoleViewModel.RoleName)
                }),
                Expires = DateTime.Now.AddMinutes(45),
                Issuer = "https://securetoken.google.com/flutter-chat-ba7c2",
                Audience = "flutter-chat-ba7c2",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
