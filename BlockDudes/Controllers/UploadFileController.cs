using BlockDudes.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlockDudes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost]
        public void UploadFile([FromForm] AssetModel model)
        {

        }
    }
}