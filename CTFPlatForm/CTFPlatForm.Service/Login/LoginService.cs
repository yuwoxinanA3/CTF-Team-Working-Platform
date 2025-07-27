using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Repository.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Service.Login
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly LoginRepository _loginRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginService(LoginRepository loginRepository) {
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<UserRes> GetUser(LoginReq req)
        {
            return await _loginRepository.GetUser(req);
        }
    }
}
