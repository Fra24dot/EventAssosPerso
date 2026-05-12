using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string email);
        Task<User> LoginAsync(string email, string password);
    }
}
