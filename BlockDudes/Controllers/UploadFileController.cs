using BlockDudes.Models;
using BlockDudes.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
            viewModel.Id = Guid.NewGuid();
            viewModel.Title = model.Title;
            viewModel.Description = model.Description;

            using (var memoryStream = new MemoryStream())
            {
                await model.FormFile.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                viewModel.Image = new AssetImage
                {
                    Bytes = imageBytes,
                    StringRepresentation =
                        $"data:{model.FormFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}"
                };
            }

            _assetService.Add(viewModel);

            return Redirect("/marketplace");
        }
    }
}