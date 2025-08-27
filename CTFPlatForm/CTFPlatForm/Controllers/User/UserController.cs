using CTFPlatForm.Core.Dto.Base;
using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Core.Other;
using CTFPlatForm.Infrastructure.Tools;
using CTFPlatForm.Service.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers.User
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
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


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize] // 启用JWT验证
        public async Task<ApiResult> GetUserById([FromBody] TextReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.GetUserById(req);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize] // 启用JWT验证
        public async Task<ApiResult> ChangeUserImage([FromBody] TextReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.ChangeUserImage(req);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize] // 启用JWT验证
        public async Task<ApiResult> ChangeUserNickname([FromBody] TextReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.ChangeUserNickname(req);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

    }
}
