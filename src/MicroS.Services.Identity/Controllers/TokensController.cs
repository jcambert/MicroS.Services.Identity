using MicroS.Services.Identity.Messsages.Commands;
using MicroS.Services.Identity.Services;
using MicroS_Common.Authentication;
using MicroS_Common.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroS.Services.Identity.Controllers
{
    [Route("")]
    [ApiController]
    [JwtAuth]
    public class TokensController : BaseController
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokensController(IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("access-tokens/{refreshToken}/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken, RefreshAccessToken command)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(command.Bind(c => c.Token, refreshToken).Token));

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken(RevokeAccessToken command)
        {
            await _accessTokenService.DeactivateCurrentAsync(
                command.Bind(c => c.UserId, UserId).UserId.ToString("N"));

            return NoContent();
        }

        [HttpPost("refresh-tokens/{refreshToken}/revoke")]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken, RevokeRefreshToken command)
        {
            await _refreshTokenService.RevokeAsync(command.Bind(c => c.Token, refreshToken).Token,
                command.Bind(c => c.UserId, UserId).UserId);

            return NoContent();
        }
    }
}
