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

    }
}
