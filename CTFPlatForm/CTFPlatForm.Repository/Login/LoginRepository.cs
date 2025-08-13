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
        public async Task<UserRes?> GetUser(LoginReq req)
        {
            // 登录验证的正确方式
            var user = await _db.Queryable<Users>()
                .Where(p => p.UserAccount == req.UserAccount && !p.IsDeleted)
                .FirstAsync();

            // 先检查用户是否存在
            if (user == null)
            {
                return null;
            }

            // 使用BCrypt验证密码
            if (BCrypt.Net.BCrypt.Verify(req.PassWord, user.Password))
            {
                // 密码正确，返回用户信息
                // 使用对象初始化器映射
                return new UserRes
                {
                    Id = user.Id,
                    Account = user.UserAccount,
                    NickName = user.NickName ?? string.Empty
                    // 映射其他属性
                };
            }
            else
            {
                // 密码错误
                return null;
            }
        }

    }
}
