using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface
{
    public interface IUpgradeService
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <returns></returns>
        public Task<bool> InitDataBase();

    }
}
