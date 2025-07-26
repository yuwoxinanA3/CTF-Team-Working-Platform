using BenchmarkDotNet.Loggers;
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
                        NickName= req.UserAccount,
                        UserAccount = req.UserAccount,
                        Password = BCrypt.Net.BCrypt.HashPassword(req.PassWord), //BCrypt加密
                        UserType = 1,
                        IsEnable = true,
                        Image = "https://img2.baidu.com/it/u=3233382939,3485050990&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500",
                        CreateDate = DateTime.Now,
                        CreateUserId ="system",
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
    }
}
