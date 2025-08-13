using CTFPlatForm.Api.Config;
using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Core.Other;
using CTFPlatForm.Infrastructure.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    /// <summary>
    /// 登录系统相关接口
    /// </summary>
    public class LoginController : BaseController
    {
        private ILoginService _loginService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public LoginController(IConfiguration configuration, ILogger<LoginController> logger, ILoginService loginService) : base(configuration, logger)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="loginReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginReq loginReq)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                UserRes user = await _loginService.GetUser(loginReq);
                if (user == null)
                {
                    return Unauthorized();
                }
                JWTHelper jWTHelper = new JWTHelper();
                var token = jWTHelper.GenerateJwtToken(user.Id,loginReq.UserAccount, _configuration["JWTSettings:ValidIssuer"], _configuration["JWTSettings:ValidAudience"], _configuration["JWTSettings:IssuerSigningKey"]);

                _logger.LogInformation("登录");
                return Ok(new { token });
            }

            return Unauthorized();
        }

    }
}
