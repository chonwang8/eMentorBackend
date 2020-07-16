using Domain.DTO.AuthDTOs;
using Domain.Models.AdminModels;
using Domain.Models.UserModels;

namespace Domain.Services.Interfaces
{
    public interface IAuthService
    {

        public string Register(UserRegisterViewModel user);
        public LoginResponseDTO GoogleLogin(UserLoginViewModel user);
        public string Login(AdminLoginModel adminLogin);
    }
}
