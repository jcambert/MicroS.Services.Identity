using MicroS.Services.Identity.Domain;
using MicroS_Common.Authentication;
using System;
using System.Threading.Tasks;

namespace MicroS.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
