using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        protected readonly ILogger<BaseController> _logger;
        public BaseController(IConfiguration configuration, ILogger<BaseController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

    }
}
