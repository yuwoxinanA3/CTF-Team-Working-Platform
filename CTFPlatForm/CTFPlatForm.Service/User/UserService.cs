using BenchmarkDotNet.Loggers;
using CTFPlatForm.Core.Dto.Base;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Entitys;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Core.Other;
using CTFPlatForm.Repository.Login;
using CTFPlatForm.Repository.User;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Service.Register
{
    public class UserService : IUserService
    {
        private UserRepository _userRepository;
        private ILogger<UserService> _logger;

        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        /// <summary>
        /// 账号密码注册用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResult> RegisterUser(RegisterReq req)
        {
            try
            {
                bool isExisting = await _userRepository.ExistingUser(req.UserAccount);
                //检查账号是否已存在
                if (isExisting)
                    return new ApiResult
                    {
                        IsSuccess = true,
                        Result = false,
                        Msg = "注册失败，账号已存在!"
                    };
                else
                {
                    Users newUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        NickName = req.UserAccount,
                        UserAccount = req.UserAccount,
                        Password = BCrypt.Net.BCrypt.HashPassword(req.PassWord), //BCrypt加密
                        UserType = 1,
                        IsEnable = true,
                        Image = "https://img2.baidu.com/it/u=3233382939,3485050990&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500",
                        CreateDate = DateTime.Now,
                        CreateUserId = "system",
                        ModifyDate = null
                    };
                    bool result = await _userRepository.AddUser(newUser);
                    return new ApiResult
                    {
                        IsSuccess = result,
                        Result = result,
                        Msg = result ? "注册成功!" : "注册失败，请重新尝试，若多次尝试失败，请联系客服!"
                    };

                }

            }
            catch (Exception ex)
            {
                // 记录日志
                _logger?.LogError(ex, "用户注册失败: {UserAccount}", req.UserAccount);

                return new ApiResult
                {
                    IsSuccess = false,
                    Result = false,
                    Msg = "系统异常，请稍后重试!"
                };
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUserById(TextReq req)
        {
            UserRes user = await _userRepository.GetUserById(req.TextContent);
            if (user != null)
            {
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = user,
                    Msg = "Success!"
                };
            }
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = null,
                    Msg = "未查询到指定用户!"
                };
        }

        public async Task<ApiResult> ChangeUserImage(TextReq req)
        {
            //req中第一个字符串为userid，第二个为新的图片url
            bool isSuccess = await _userRepository.ChangeUserImage(req.TextContent, req.TextContent2);
            if (isSuccess)
                return new ApiResult
                {
                    IsSuccess = true,
                    Result=true
                };
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = false
                };
        }

        public async Task<ApiResult> ChangeUserNickname(TextReq req)
        {
            //req中第一个字符串为userid，第二个为新的昵称URL
            bool isSuccess = await _userRepository.ChangeUserNickname(req.TextContent, req.TextContent2);
            if (isSuccess)
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = true
                };
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = false
                };
        }
    }
}
