using CTFPlatForm.Core.Interface;
using CTFPlatForm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Service.Upgrade
{
    public class UpgradeService:BaseService, IUpgradeService
    {
        private UpgradeRepository _upgradeRepository;

        public UpgradeService(UpgradeRepository upgradeRepository)
        {
            _upgradeRepository = upgradeRepository;
        }

        public async Task<bool> InitDataBase()
        {
            return await _upgradeRepository.InitDataBase();
        }
    }
}
