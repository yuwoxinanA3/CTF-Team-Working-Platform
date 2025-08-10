using CTFPlatForm.Core.Interface;
using CTFPlatForm.Service.Upload;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Service
{
    // FileUploadServiceFactory.cs
    public interface IFileUploadServiceFactory
    {
        IFileUploadService GetUploadService(string type);
    }

    public class FileUploadServiceFactory : IFileUploadServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileUploadServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileUploadService GetUploadService(string type)
        {
            return type.ToLower() switch
            {
                "local" => _serviceProvider.GetRequiredService<LocalFileUploadService>(),
                "cloud" => _serviceProvider.GetRequiredService<CloudFileUploadService>(),
                _ => _serviceProvider.GetRequiredService<LocalFileUploadService>() // 默认使用本地存储
            };
        }
    }
}
