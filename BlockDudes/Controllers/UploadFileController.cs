using BlockDudes.Models;
using BlockDudes.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BlockDudes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly AssetService _assetService;

        public UploadFileController(AssetService assetService)
        {
            _assetService = assetService;
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] AssetModel model)
        {
            var viewModel = new AssetViewModel();
            viewModel.Title = model.Title;
            viewModel.Description = model.Description;

            using (var memoryStream = new MemoryStream())
            {
                await model.FormFile.CopyToAsync(memoryStream);

                viewModel.File = memoryStream.ToArray();
            }

            _assetService.Add(viewModel);

            return Redirect("/marketplace");
        }
    }
}