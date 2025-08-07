using CTFPlatForm.Core.Dto.Base;
using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Core.Interface.Team;
using CTFPlatForm.Infrastructure.Tools;
using CTFPlatForm.Service.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    public class TeamController : BaseController
    {
        private ITeamService _teamService;

        public TeamController(IConfiguration configuration, ILogger<TeamController> logger, ITeamService teamService) : base(configuration, logger)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// 判断战队名是否可用
        /// </summary>
        /// <param name="textReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> IsAvailableTeamName([FromBody] TextReq textReq)
        {
            return await _teamService.IsAvailableTeamName(textReq.TextContent);
        }



    }
}
