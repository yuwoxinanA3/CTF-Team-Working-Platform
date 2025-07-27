using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface.Login
{
    public interface ILoginService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public  Task<UserRes> GetUser(LoginReq req);

    }
}
