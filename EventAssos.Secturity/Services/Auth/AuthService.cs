using EventAssos.Core.Interfaces.Repositories.Data;
using EventAssos.Core.Interfaces.Services.Auth;
using EventAssos.Core.Interfaces.Services.Tools;
using EventAssos.Domain.Entities;
using EventAssos.Domain.Enums;
using EventAssos.Security.Services.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EventAssos.Security.Services.Auth
{
    public class AuthService(IUserRepository _userRepository,
        IPasswordHacherService _passwordHacherService,
        IPasswordGeneratorService _passwordGeneratorService) : IAuthService
    {
        public async Task<User?> RegisterAsync(string email)
        {
            var userExisting = await _userRepository.GetByEmailAsync(email);

            if (userExisting is not null)
            {
                throw new UnauthorizedAccessException("The email already exist");
            }

            var generatedPassword = _passwordGeneratorService.RandomPassword();
            var hachedPassword = _passwordHacherService.HachPassword(generatedPassword);


            User user = new User()
            {
                Email = email,
                Password = hachedPassword,
                Role = UserRole.User

            };

            return await _userRepository.AddAsync(user);
        }
    }
}
