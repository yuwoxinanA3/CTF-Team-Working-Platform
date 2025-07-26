using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Core.Other;
using CTFPlatForm.Infrastructure.Tools;
using CTFPlatForm.Service.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers.User
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UserController(IConfiguration configuration, ILogger<UserController> logger, IUserService userService) : base(configuration, logger)
        {
            _userService = userService;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="registerReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RegisterUser([FromBody] RegisterReq registerReq)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.RegisterUser(registerReq);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

    }
}
