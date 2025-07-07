using CTFPlatForm.Application.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Application.SystemControllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        public LoginController(IConfiguration configuration, ILogger<LoginController> logger) : base(configuration, logger)
        {

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
