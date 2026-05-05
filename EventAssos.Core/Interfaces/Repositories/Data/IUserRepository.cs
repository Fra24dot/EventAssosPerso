using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Repositories.Data
{
    public interface IUserRepository
    {

        Task<User?> AddAsync (User user);
        Task<User?> GetByEmailAsync(string email);


    }
}
