using MicroS_Common.Messages;
using Newtonsoft.Json;
using System;

namespace MicroS.Services.Identity.Messsages.Commands
{
    public class RevokeRefreshToken : ICommand
    {
        public Guid UserId { get; }
        public string Token { get; }

        [JsonConstructor]
        public RevokeRefreshToken(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}
