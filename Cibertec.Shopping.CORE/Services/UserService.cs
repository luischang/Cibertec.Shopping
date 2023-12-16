using Cibertec.Shopping.CORE.DTOs;
using Cibertec.Shopping.CORE.Entities;
using Cibertec.Shopping.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cibertec.Shopping.CORE.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;
        public UserService(IUserRepository userRepository, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<bool> SignUp(UserInsertDTO userInsertDTO)
        {
            var existsUser = await _userRepository.GetByEmail(userInsertDTO.Email);
            if (existsUser)
                return false;

            var user = new User()
            {
                FirstName = userInsertDTO.FirstName,
                LastName = userInsertDTO.LastName,
                Email = userInsertDTO.Email,
                Country = userInsertDTO.Country,
                Address = userInsertDTO.Address,
                DateOfBirth = userInsertDTO.DateOfBirth,
                Password = userInsertDTO.Password,
                IsActive = true,
                Type = "U"
            };

            return await _userRepository.Insert(user);
        }

        public async Task<UserAuthenticationDTO> SignIn(UserLoginDTO userLoginDTO)
        {
            var user = await _userRepository.GetUserByCredentials(userLoginDTO.Email, userLoginDTO.Password);
            if (user == null)
                return null;

            //TODO: Generar un JWT
            var token = _jwtService.GenerateJWToken(user);
            var userAuth = new UserAuthenticationDTO()
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };

            return userAuth;
        }
    }
}
