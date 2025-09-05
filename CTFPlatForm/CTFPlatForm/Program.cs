
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CTFPlatForm.Infrastructure.Tools;
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
            // 使用 Autofac 作为 DI 容器
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // 配置 Autofac 容器
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new ServiceModule());
            });

            // 加载多个嵌入式 JSON 配置文件,将分开的配置文件整合到一起
            var assembly = typeof(CTFPlatForm.Infrastructure.Tools.ConfigurationBuilderExtensions).Assembly;
            builder.Configuration
                .AddEmbeddedJsonFile(assembly, "Configuration.JWT.json")
                .AddEmbeddedJsonFile(assembly, "Configuration.Database.json");

            // 添加 CORS 服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVueApp",
                    policy =>
                    {
                        policy.WithOrigins("http://127.0.0.1:5001", "http://localhost:5001")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

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
                    ValidateIssuerSigningKey = bool.Parse(builder.Configuration["JWTSettings:ValidateIssuerSigningKey"]),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:IssuerSigningKey"])),
                    ValidateIssuer = bool.Parse(builder.Configuration["JWTSettings:ValidateIssuer"]),
                    ValidIssuer = builder.Configuration["JWTSettings:ValidIssuer"],
                    ValidateAudience = bool.Parse(builder.Configuration["JWTSettings:ValidateAudience"]),
                    ValidAudience = builder.Configuration["JWTSettings:ValidAudience"],
                    ValidateLifetime = bool.Parse(builder.Configuration["JWTSettings:ValidateLifetime"]),
                    ClockSkew = TimeSpan.FromSeconds(int.Parse(builder.Configuration["JWTSettings:ClockSkew"])),
                    RequireExpirationTime = bool.Parse(builder.Configuration["JWTSettings:RequireExpirationTime"])
                };

                // 禁用默认的claim类型映射
                options.MapInboundClaims = false;
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
            builder.Services.AddSwaggerGen(s =>
            {
                //开启Swagger文档注释
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "CTFPlatForm.Api.xml");
                s.IncludeXmlComments(xmlPath, true);
            });

            //实例化APP
            var app = builder.Build();

            //开发环境配置
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // 开发环境允许所有来源
            if (app.Environment.IsDevelopment())
            {
                app.UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }
            else
            {
                app.UseCors("AllowVueApp");
            }
            // 配置静态文件中间件，确保可以访问上传的文件
            app.UseStaticFiles();


            // 启用认证和授权中间件
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            //启动实例程序
            app.Run();
        }
    }
}
