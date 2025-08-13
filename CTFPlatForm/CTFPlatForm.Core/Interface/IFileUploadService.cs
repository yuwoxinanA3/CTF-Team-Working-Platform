using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface
{
    public interface IFileUploadService
    {
        Task<string> UploadAvatarAsync(IFormFile file);
    }

}
