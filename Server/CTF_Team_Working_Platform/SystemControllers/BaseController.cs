using CTFPlatForm.Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CTFPlatForm.Application.SystemControllers
{
    [ApiController]
    [Route("[controller]")]
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
