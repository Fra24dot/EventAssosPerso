using EventAssos.Core.Interfaces.Repositories.Data;
using EventAssos.Domain.Entities;
using EventAssos.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Infrastructure.Repositories.Data
{
    public class UserRepository(EventAssosContext _context) : IUserRepository
    {
        public async Task<User?> AddAsync(User user)
        {
            if (user is null) return null;

            await _context.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (email is null) return null;
            return await _context.Users.FirstOrDefaultAsync(e=> e.Email == email);
        }
    }
}
