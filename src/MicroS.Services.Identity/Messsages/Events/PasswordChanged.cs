using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Events
{
    public class PasswordChanged : IEvent
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public PasswordChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}
