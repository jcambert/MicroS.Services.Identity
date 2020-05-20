using MicroS_Common.Dispatchers;
using MicroS_Common.RabbitMq;
using MicroS_Common.Services.Identity.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MicroS.Services.Identity.Controllers
{
    [Route("")]
    public class IdentityController : BaseIdentityController
    {
        public IdentityController(MicroS_Common.Services.Identity.Services.IIdentityService identityService, MicroS_Common.Services.Identity.Services.IRefreshTokenService refreshTokenService, IBusPublisher busPublisher, IDispatcher dispatcher, IConfiguration configuration) : base(identityService, refreshTokenService, busPublisher, dispatcher, configuration)
        {
        }
    }
   
}
