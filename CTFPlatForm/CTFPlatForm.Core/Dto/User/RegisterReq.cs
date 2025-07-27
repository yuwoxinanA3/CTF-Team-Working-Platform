using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Dto.User
{
    /// <summary>
    /// 账号密码注册请求
    /// </summary>
    public class RegisterReq
    {
        [Required(ErrorMessage = "账号缺失")]
        public string UserAccount { get; set; }
        [Required(ErrorMessage = "密码缺失")]
        public string PassWord { get; set; }
    }
}
