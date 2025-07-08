using CTFPlatForm.Core.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Entitys
{
    public class Users : IEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string UserAccount { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Password { get; set; }
        /// <summary>
        /// 用户类型（0=超级管理员，1=普通用户）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int UserType { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsEnable { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Image { get; set; }
    }
}
