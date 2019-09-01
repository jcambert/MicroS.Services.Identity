using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Events
{
    public class RefreshAccessTokenRejected : IRejectedEvent
    {
        public Guid UserId { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public RefreshAccessTokenRejected(Guid userId, string reason, string code)
        {
            UserId = userId;
            Reason = reason;
            Code = code;
        }
    }
}
