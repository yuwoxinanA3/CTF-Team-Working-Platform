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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // 验证用户凭据
            if (model.Username == "validUser" && model.Password == "validPassword")
            {
                var token = ToolHelper.GenerateJwtToken(model.Username, _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], _configuration["Jwt:Key"]);
                return Ok(new { token });
            }

            return Unauthorized();
        }

    }
}
