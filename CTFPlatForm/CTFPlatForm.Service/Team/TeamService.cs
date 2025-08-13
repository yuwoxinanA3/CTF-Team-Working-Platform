using CTFPlatForm.Core.Dto.Team;
using CTFPlatForm.Core.Entitys;
using CTFPlatForm.Core.Interface.Team;
using CTFPlatForm.Repository.Login;
using CTFPlatForm.Repository.Team;
using CTFPlatForm.Repository.User;
using Perfolizer.Mathematics.Selectors;
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

        public async Task<bool> CreateCTFTeam(string UserId,CreateTeamReq createTeamReq)
        {
            Teams newTeam = new()
            {
                Id = Guid.NewGuid().ToString(),
                TeamName = createTeamReq.TeamName,
                TeamIcon = createTeamReq.TeamIcon,
                Declaration = createTeamReq.Declaration,
                TeamIntroduction = createTeamReq.TeamIntroduction,
                EstablishmentTime = createTeamReq.EstablishmentTime,
                TeamPoints = 0,
                TeamLeader = UserId,
                TeamEmail = createTeamReq.TeamEmail,
                TeamWebsite = createTeamReq.TeamWebsite,
                Country = createTeamReq.Country,
                City = createTeamReq.City,
                University = string.Empty,
                MemberCount = 1,
                IsPublic=createTeamReq.IsPublic,
                //标准字段
                CreateUserId = UserId,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };
            return await _teamRepository.AddTeam(newTeam);
        }

    }
}
