using Domain.Helper.DataObjects;
using Domain.ViewModels.AdminModels;
using Domain.ViewModels.UserModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public string CreateAdminAccessToken(AdminModel adminViewModel)
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



        public string CreateUserAccessToken(UserAuthModel userAuthModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", userAuthModel.Id.ToString()),
                    new Claim("email", userAuthModel.Email),
                    new Claim("role-name", userAuthModel.RoleName)
                }),
                //  Expires = DateTime.Now.AddMinutes(45),
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

        public UserAuthModel TokenReader(string jwtToken)
        {
            UserAuthModel userAuth = null;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityToken jsonToken = handler.ReadToken(jwtToken);
            JwtSecurityToken tokenS = handler.ReadToken(jwtToken) as JwtSecurityToken;

            userAuth = new UserAuthModel
            {
                Id = new Guid(tokenS.Claims.First(claim => claim.Type == "id").Value),
                Email = tokenS.Claims.First(claim => claim.Type == "email").Value,
                RoleName = tokenS.Claims.First(claim => claim.Type == "role-name").Value
            };

            return userAuth;
        }
    }
}
