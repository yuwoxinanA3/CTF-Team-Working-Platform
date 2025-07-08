
using CTFPlatForm.Core.Interface;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Infrastructure.Tools;
using CTFPlatForm.Repository;
using CTFPlatForm.Repository.Login;
using CTFPlatForm.Service.Login;
using CTFPlatForm.Service.Upgrade;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using System.Text;

namespace CTFPlatForm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 加载多个嵌入式 JSON 配置文件,将分开的配置文件整合到一起
            var assembly = typeof(CTFPlatForm.Infrastructure.Tools.ConfigurationBuilderExtensions).Assembly;
            builder.Configuration
                .AddEmbeddedJsonFile(assembly, "Configuration.JWT.json")
                .AddEmbeddedJsonFile(assembly, "Configuration.Database.json");

            // 添加服务
            builder.Services.AddAuthorization();
            //添加控制器服务
            builder.Services.AddControllers();

            #region 添加JWT认证服务
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWTSettings:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWTSettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:IssuerSigningKey"]))
                };
            });
            #endregion

            #region 添加 SqlSugar
            builder.Services.AddScoped<ISqlSugarClient>(provider =>
            {
                var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
                return new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = connectionString,
                    DbType = DbType.MySql, // 根据你的数据库类型修改，如 MySql、Sqlite 等
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute // 使用实体类属性映射字段
                });
            });
            #endregion

            // 了解有关配置Swagger/OpenAPI的更多信息
            // 微软官网链接 https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 注册服务
            builder.Services.AddScoped<LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddScoped<UpgradeRepository>();
            builder.Services.AddScoped<IUpgradeService, UpgradeService>();




            //实例化APP
            var app = builder.Build();

            //开发环境配置
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            // 启用认证和授权中间件
            app.UseAuthentication();
            app.MapControllers();

            //启动实例程序
            app.Run();
        }
    }
}
