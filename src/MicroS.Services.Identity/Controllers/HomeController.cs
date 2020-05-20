using MicroS_Common.Controllers;
using MicroS_Common.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MicroS.Services.Identity.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(IDispatcher dispatcher, IConfiguration configuration) : base(dispatcher, configuration)
        {
        }
    }
}
