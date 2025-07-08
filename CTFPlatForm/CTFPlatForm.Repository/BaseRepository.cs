using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Repository
{
    /// <summary>
    /// 仓储类基类
    /// </summary>
    public class BaseRepository
    {
        /// <summary>
        /// SqlSugarClient 实例，db
        /// </summary>
        protected readonly ISqlSugarClient _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        public BaseRepository(ISqlSugarClient db)
        {
            _db = db;
        }

    }
}
