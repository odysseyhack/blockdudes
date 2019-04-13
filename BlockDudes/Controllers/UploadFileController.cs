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
        private readonly IAccountsService _accountsService;
        private readonly IHashService _hashService;
        private readonly IIpfsProviderService _ipfsProviderService;

        public UploadFileController(
            AssetService assetService,
            IAccountsService accountsService,
            IHashService hashService,
            IIpfsProviderService ipfsProviderService)
        {
            _assetService = assetService;
            _accountsService = accountsService;
            _hashService = hashService;
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
                Address = _accountsService.GetCurrentAccount().Address
            };

            if (model.FormFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {

                    await model.FormFile.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var imageHash = _hashService.GetHash(imageBytes);
                    await _ipfsProviderService.AddAsync(imageHash, imageBytes);

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