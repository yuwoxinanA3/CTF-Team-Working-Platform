﻿using CTFPlatForm.Core.Entitys;
using Perfolizer.Mathematics.Selectors;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Repository
{
    public class UpgradeRepository:BaseRepository
    {
        public UpgradeRepository(SqlSugar.ISqlSugarClient db) : base(db)
        {


        }

        public async Task<bool> InitDataBase()
        {
            //创建数据库
            _db.DbMaintenance.CreateDatabase();

            //通过反射，加载程序集，读取所有类型，然后根据实体创建表
            string nameSpace = "CTFPlatForm.Core.Entitys";
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "CTFPlatForm.Core.dll").GetTypes().Where(p => p.Namespace == nameSpace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);

            //初始化超级管理员
            Core.Entitys.Users user = new Core.Entitys.Users()
            {
                Id = Guid.NewGuid().ToString(),
                UserAccount = "admin",
                NickName = "超级管理员",
                Password =  BCrypt.Net.BCrypt.HashPassword("123456"),
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认的超级管理员",
                CreateDate = DateTime.Now,
                CreateUserId = "system"
            };
            return await _db.Insertable(user).ExecuteCommandIdentityIntoEntityAsync();
        }

        public async Task<bool> UpgradeDataBase()
        {
            // SqlSugar 的 InitTables 方法本身就支持增量更新
            // 它会自动检测表是否存在，只创建不存在的表
            string nameSpace = "CTFPlatForm.Core.Entitys";
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "CTFPlatForm.Core.dll")
                .GetTypes()
                .Where(p => p.Namespace == nameSpace)
                .ToArray();

            // 直接调用，会自动处理新增表和现有表
            try
            {
                _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
