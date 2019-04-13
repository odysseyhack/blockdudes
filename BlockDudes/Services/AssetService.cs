using BlockDudes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public class AssetService
    {
        private readonly IAccountsService _accountsService;
        private readonly IIpfsProviderService _ipfsProviderService;

        public AssetService(
            IAccountsService accountsService,
            IIpfsProviderService ipfsProviderService
        )
        {
            _accountsService = accountsService;
            _ipfsProviderService = ipfsProviderService;
        }

        public async Task<List<AssetViewModel>> GetAll()
        {
            var result = new List<AssetViewModel>();
            var length = await _accountsService.GetAllItemsLength();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    var model = await GetViewModelByIndex(i);

                    result.Add(model);
                } catch (Exception ex)
                {
                    // ignore it
                }
            }
            return result;
        }

        private async Task<AssetViewModel> GetViewModelByIndex(int index)
        {
            var item = await _accountsService.GetItem(index);

            var descriptionJson = await _ipfsProviderService.GetTextAsync(item.DescriptionHash);

            var model = JsonConvert.DeserializeObject<AssetViewModel>(descriptionJson);
            model.Token = item.TokenId;
            model.Link = $"https://ipfs.infura.io/ipfs/{item.FileHash}";

            return model;
        }
    }
}
