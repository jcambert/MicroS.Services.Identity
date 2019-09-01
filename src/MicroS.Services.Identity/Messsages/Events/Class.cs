using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Events
{
    public class SignedUp : IEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignedUp(Guid userId, string email, string role)
        {
            UserId = userId;
            Email = email;
            Role = role;
        }
    }
}
