
using CTFPlatForm.Core.Interface;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Core.Interface.Team;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Infrastructure.Tools;
using CTFPlatForm.Repository;
using CTFPlatForm.Repository.Login;
using CTFPlatForm.Repository.Team;
using CTFPlatForm.Repository.User;
using CTFPlatForm.Service;
using CTFPlatForm.Service.Login;
using CTFPlatForm.Service.Register;
using CTFPlatForm.Service.Team;
using CTFPlatForm.Service.Upgrade;
using CTFPlatForm.Service.Upload;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetTaste;
using SqlSugar;
using System.Diagnostics;
using System.Text;

namespace CTFPlatForm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ���ض��Ƕ��ʽ JSON �����ļ�,���ֿ��������ļ����ϵ�һ��
            var assembly = typeof(CTFPlatForm.Infrastructure.Tools.ConfigurationBuilderExtensions).Assembly;
            builder.Configuration
                .AddEmbeddedJsonFile(assembly, "Configuration.JWT.json")
                .AddEmbeddedJsonFile(assembly, "Configuration.Database.json");

            // ��ӷ���
            builder.Services.AddAuthorization();
            //��ӿ���������
            builder.Services.AddControllers();

            #region ���JWT��֤����
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

            #region ��� SqlSugar
            builder.Services.AddScoped<ISqlSugarClient>(provider =>
            {
                var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
                return new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = connectionString,
                    DbType = DbType.MySql, // ����������ݿ������޸ģ��� MySql��Sqlite ��
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute // ʹ��ʵ��������ӳ���ֶ�
                });
            });
            #endregion

            // �˽��й�����Swagger/OpenAPI�ĸ�����Ϣ
            // ΢��������� https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                //����Swagger�ĵ�ע��
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "CTFPlatForm.Api.xml");
                s.IncludeXmlComments(xmlPath, true);
            });

            // ע�����
            builder.Services.AddScoped<LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddScoped<UpgradeRepository>();
            builder.Services.AddScoped<IUpgradeService, UpgradeService>();

            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<TeamRepository>();
            builder.Services.AddScoped<ITeamService, TeamService>();

            // �ϴ�����
            builder.Services.AddScoped<LocalFileUploadService>();
            builder.Services.AddScoped<CloudFileUploadService>();
            builder.Services.AddScoped<IFileUploadServiceFactory, FileUploadServiceFactory>();


            //ʵ����APP
            var app = builder.Build();

            //������������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // ���þ�̬�ļ��м����ȷ�����Է����ϴ����ļ�
            app.UseStaticFiles();

            // ������֤����Ȩ�м��
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            //����ʵ������
            app.Run();
        }
    }
}
