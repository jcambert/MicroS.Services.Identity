using MicroS_Common.Authentication;
using MicroS_Common.Dispatchers;
using MicroS_Common.Services.Identity.Controllers;
using Microsoft.Extensions.Configuration;

namespace MicroS.Services.Identity.Controllers
{
    public class TokensController : BaseTokensController
    {
        public TokensController(IAccessTokenService accessTokenService, MicroS_Common.Services.Identity.Services.IRefreshTokenService refreshTokenService, IDispatcher dispatcher, IConfiguration configuration) : base(accessTokenService, refreshTokenService, dispatcher, configuration)
        {
        }
    }
}
