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

        /// <summary>
        /// 可选文本2
        /// </summary>
        public string? TextContent2 { get; set; }

        /// <summary>
        /// 可选文本3
        /// </summary>
        public string? TextContent3 { get; set; }
    }
}
