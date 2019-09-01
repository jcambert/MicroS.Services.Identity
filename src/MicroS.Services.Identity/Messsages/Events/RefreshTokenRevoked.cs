using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Events
{
    public class RefreshTokenRevoked : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public RefreshTokenRevoked(Guid userId)
        {
            UserId = userId;
        }
    }
}
