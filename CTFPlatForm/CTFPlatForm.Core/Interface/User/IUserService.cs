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
        public Task<ApiResult> RegisterUser(RegisterReq req);
    }
}
