using CTFPlatForm.Core.Interface;
using CTFPlatForm.Core.Interface.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    /// <summary>
    /// 数据库 升级接口
    /// </summary>
    public class UpgradeController : BaseController
    {
        private IUpgradeService _upgradeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UpgradeController(IConfiguration configuration, ILogger<LoginController> logger, IUpgradeService upgradeService) : base(configuration, logger)
        {
            _upgradeService = upgradeService;
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> InitDataBase()
        {
            return await _upgradeService.InitDataBase();
        }
    }

}