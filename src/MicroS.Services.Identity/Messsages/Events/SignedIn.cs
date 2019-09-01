using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Events
{
    public class SignedIn : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public SignedIn(Guid userId)
        {
            UserId = userId;
        }
    }
}
