using LargeFileUpload.Business.ModelBusiness;
using Microsoft.AspNetCore.Mvc;

namespace LargeFileUpload.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadBusiness _fileUploadBusiness;
        private readonly ResponseContext _responseData;
        public class ResponseContext
        {
            public dynamic Data { get; set; }
            public bool IsSuccess { get; set; } = true;
            public string ErrorMessage { get; set; }
        }

        public FileUploadController(IFileUploadBusiness fileUploadBusiness)
        {
            _fileUploadBusiness = fileUploadBusiness;
            _responseData = new ResponseContext();
        }

        [HttpPost("UploadChunks")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(string id, string fileName)
        {

            var body = Request.Body;

            await _fileUploadBusiness.UploadChunks(id, fileName, body);

            _responseData.IsSuccess = true;

            return Ok(_responseData);
        }

        [HttpPost("UploadComplete")]
        public async Task<IActionResult> PostComplete(string fileName)
        {

            
            await _fileUploadBusiness.UploadComplete(fileName);

            _responseData.IsSuccess = true;

            return Ok(_responseData);
        }
    }
}