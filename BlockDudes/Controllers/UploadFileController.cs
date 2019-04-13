using BlockDudes.Models;
using BlockDudes.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IAccountsService _accountsService;
        private readonly IIpfsProviderService _ipfsProviderService;

        public UploadFileController(
            AssetService assetService,
            IAccountsService accountsService,
            IIpfsProviderService ipfsProviderService)
        {
            _assetService = assetService;
            _accountsService = accountsService;
            _ipfsProviderService = ipfsProviderService;
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] AssetModel model)
        {
            var viewModel = new AssetViewModel
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                UploadTime = DateTime.Now,
                Address = _accountsService.GetCurrentAccount().Address
            };

            if (model.FormFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.FormFile.CopyToAsync(memoryStream);
                    var uploadImageBytes = memoryStream.ToArray();
                    var uploadImageDescription = JsonConvert.SerializeObject(viewModel);

                    var imageHash = await _ipfsProviderService.AddAsync(uploadImageBytes);
                    var imageDescriptionHash = await _ipfsProviderService.AddTextAsync(uploadImageDescription);
                    await _accountsService.UploadAssetAsync(imageHash, imageDescriptionHash);

                    var imageBytes = await _ipfsProviderService.GetAsync(imageHash);
                    var imageDescription = await _ipfsProviderService.GetTextAsync(imageDescriptionHash);

                    var imageDescriptionModel = JsonConvert.DeserializeObject<AssetViewModel>(imageDescription);

                    viewModel.Image = new AssetImage
                    {
                        Bytes = imageBytes,
                        StringRepresentation =
                            $"data:{model.FormFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}"
                    };
                }
            }

            _assetService.Add(viewModel);

            return Redirect("/marketplace");
        }
    }
}