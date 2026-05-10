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
        IPasswordGeneratorService _passwordGeneratorService,
        IEmailService _emailService) : IAuthService
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

            var newUser = await _userRepository.AddAsync(user);

            string subject = "Bienvenue - Votre mot de passe provisoire";

            //Corps du message 
            string body = $@"
            <html>
                <body>
                    <h2>Bienvenue sur EventAssos !</h2>
                    <p>Votre compte a été créé avec succès.</p>
                    <p>Votre identifiant: <strong>{user.Email}</strong></p>
                    <p>Voici votre mot de passe provisoire : <strong>{generatedPassword}</strong></p>
                    <p>Nous vous conseillons de le modifier dès votre première connexion.</p>
                </body>
            </html>";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            return newUser;  
        }
    }
}
