using MicroS.Services.Identity.Domain;
using System;
using System.Threading.Tasks;

namespace MicroS.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
