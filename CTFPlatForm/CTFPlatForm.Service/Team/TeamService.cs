using CTFPlatForm.Core.Interface.Team;
using CTFPlatForm.Repository.Login;
using CTFPlatForm.Repository.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Service.Team
{
    public class TeamService:BaseService,ITeamService
    {
        private readonly TeamRepository _teamRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public  async Task<bool> IsAvailableTeamName(string TeamName)
        {
            return await _teamRepository.IsAvailableTeamName(TeamName);
        }

    }
}
