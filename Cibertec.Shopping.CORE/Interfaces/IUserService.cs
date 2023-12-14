using Cibertec.Shopping.CORE.DTOs;

namespace Cibertec.Shopping.CORE.Interfaces
{
    public interface IUserService
    {
        Task<UserAuthenticationDTO> SignIn(UserLoginDTO userLoginDTO);
        Task<bool> SignUp(UserInsertDTO userInsertDTO);
    }
}