using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Dto.User
{
    public class ChangePwdReq
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
