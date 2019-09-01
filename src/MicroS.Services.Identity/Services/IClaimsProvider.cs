using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroS.Services.Identity.Services
{
    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetAsync(Guid userId);
    }
}
