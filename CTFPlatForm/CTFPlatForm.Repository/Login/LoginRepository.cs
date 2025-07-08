using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Repository.Login
{
    /// <summary>
    /// 登录仓储类
    /// </summary>
    public class LoginRepository : BaseRepository
    {
        /// <summary>
        /// 登录仓储类构造函数
        /// </summary>
        /// <param name="db"></param>
        public LoginRepository(ISqlSugarClient db) : base(db)
        {

        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<UserRes> GetUser(LoginReq req)
        {
            var user = await _db.Queryable<Users>()
                .Where(p => p.UserAccount == req.UserAccount && p.Password == req.PassWord)
                .Select(p => new UserRes() { }, true).FirstAsync();
            return user;
        }



    }
}
