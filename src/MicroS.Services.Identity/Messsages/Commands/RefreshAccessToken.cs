﻿using MicroS_Common.Messages;
using Newtonsoft.Json;

namespace MicroS.Services.Identity.Messsages.Commands
{
    public class RefreshAccessToken : ICommand
    {
        public string Token { get; }

        [JsonConstructor]
        public RefreshAccessToken(string token)
        {
            Token = token;
        }
    }
}
