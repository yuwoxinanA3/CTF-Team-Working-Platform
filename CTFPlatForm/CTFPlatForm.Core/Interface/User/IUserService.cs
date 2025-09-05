using CTFPlatForm.Core.Dto.Base;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface.User
{
    public interface IUserService
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Task<ApiResult> RegisterUser(RegisterReq req);

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<ApiResult> GetUserById(string userId);

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userImageUrl"></param>
        /// <returns></returns>
        public Task<ApiResult> ChangeUserImage(string userId,string userImageUrl);

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newNickName"></param>
        /// <returns></returns>
        public Task<ApiResult> ChangeUserNickname(string userId, string newNickName);

        /// <summary>
        /// 修改用户密码(旧密码)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public Task<ApiResult> ChangePwdByOldPwd(string userId, ChangePwdReq req);

    }
}
