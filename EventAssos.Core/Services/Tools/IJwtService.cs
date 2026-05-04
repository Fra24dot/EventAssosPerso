using EventAssos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Services.Tools
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
