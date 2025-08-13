using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Repository.Team
{
    /// <summary>
    /// 战队仓储类
    /// </summary>
    public class TeamRepository : BaseRepository
    {
        #region 构造函数
        public TeamRepository(ISqlSugarClient db) : base(db)
        {


        }
        #endregion

        /// <summary>
        /// 判断战队名是否可用
        /// </summary>
        /// <param name="TeamName"></param>
        /// <returns></returns>
        public async Task<bool> IsAvailableTeamName(string TeamName)
        {
            var exists = await _db.Queryable<Teams>()
                .AnyAsync(p => p.TeamName == TeamName);
            return !exists; 
        }

        public async Task<bool> AddTeam(Teams team)
        {
            return await _db.Insertable(team).ExecuteCommandAsync() > 0;
        }
    }
}
