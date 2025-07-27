using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Common
{
    public class IEntity:IBase
    {
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Description { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsDeleted { get; set; }
    }
}
