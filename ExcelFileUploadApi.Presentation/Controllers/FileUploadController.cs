using ExcelFileUploadApi.Application.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelFileUploadApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IExcelService _excelService;
        private readonly ILoggingService _logger;
        public FileUploadController(IBackgroundJobClient backgroundJobClient, IExcelService excelService , ILoggingService loggingService)
        {
            _backgroundJobClient = backgroundJobClient;
            _excelService = excelService;
            _logger = loggingService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _logger.LogInformation("Upload File Called");
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                // تبدیل MemoryStream به آرایه بایتی
                var fileBytes = stream.ToArray();

                //_backgroundJobClient.Enqueue(() => Task.Run(() => _excelService.ProcessExcelFileAsync(fileBytes)));


                // ارسال آرایه بایتی به عنوان پارامتر به متد Hangfire
                _backgroundJobClient.Enqueue(() => _excelService.ProcessExcelFileAsync(fileBytes));
            }

            return Ok("File uploaded and processing started.");
        }
    }
}