using CTFPlatForm.Core.Dto;
using CTFPlatForm.Infrastructure.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    public class LoginController : BaseController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public LoginController(IConfiguration configuration, ILogger<LoginController> logger) : base(configuration, logger)
        {

        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="loginReq"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] LoginReq loginReq)
        {
            // 验证用户凭据
            if (loginReq.UserAccount == "li" && loginReq.PassWord == "123456")
            {
                JWTHelper jWTHelper = new JWTHelper();

                var token = jWTHelper.GenerateJwtToken(loginReq.UserAccount, _configuration["JWTSettings:ValidIssuer"], _configuration["JWTSettings:ValidAudience"], _configuration["JWTSettings:IssuerSigningKey"]);
                return Ok(new { token });
            }

            return Unauthorized();
        }

    }
}
