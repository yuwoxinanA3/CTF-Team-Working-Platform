using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Dto.Base
{
    public class TextReq
    {
        [Required(ErrorMessage = "内容缺失")]
        public string TextContent { get; set; }
    }
}
