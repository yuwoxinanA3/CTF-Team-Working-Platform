using CTFPlatForm.Core.Dto.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface.Team
{
    public interface ITeamService:IBaseService
    {
        /// <summary>
        /// 判断战队名是否可用
        /// </summary>
        /// <param name="TeamName"></param>
        /// <returns></returns>
        public Task<bool> IsAvailableTeamName(string TeamName);

        /// <summary>
        /// 创建队伍
        /// </summary>
        /// <param name="UserId">创建用户编号(默认当前登录用户编号)</param>
        /// <param name="createTeamReq"></param>
        /// <returns></returns>
        public Task<bool> CreateCTFTeam(string UserId, CreateTeamReq createTeamReq);
    }
}
