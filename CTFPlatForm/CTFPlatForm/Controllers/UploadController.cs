using CTFPlatForm.Infrastructure.Tools;
using CTFPlatForm.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    public class UploadController : BaseController
    {
        #region 构造函数
        private readonly IFileUploadServiceFactory _uploadServiceFactory;

        public UploadController(IConfiguration configuration, ILogger<UploadController> logger, IFileUploadServiceFactory uploadServiceFactory) : base(configuration, logger)
        {
            _uploadServiceFactory = uploadServiceFactory;
        }
        #endregion

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] UploadAvatarRequest request)
        {
            try
            {
                if (request.Avatar == null)
                {
                    return BadRequest(new { message = "请选择要上传的文件" });
                }

                // 获取上传服务
                var uploadService = _uploadServiceFactory.GetUploadService(request.UploadType ?? "local");

                // 上传文件
                var url = await uploadService.UploadAvatarAsync(request.Avatar);

                return Ok(new { avatar = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"上传失败: {ex.Message}" });
            }
        }

        // 请求模型
        public class UploadAvatarRequest
        {
            public IFormFile Avatar { get; set; }

            public string UploadType { get; set; } // "local" 或 "cloud"
        }
    }
}
