using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Events
{
    public class AccessTokenRefreshed : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public AccessTokenRefreshed(Guid userId)
        {
            UserId = userId;
        }
    }
}
