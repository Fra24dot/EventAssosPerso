using System;
using System.Collections.Generic;
using System.Text;

namespace EventAssos.Core.Interfaces.Services.Tools
{
    public interface IPasswordGeneratorService
    {
        string RandomPassword(int length = 12);
    }
}
