using BlockDudes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Task<AssetViewModel>[] tasks = new Task<AssetViewModel>[length - 8];
            for (int i = 8; i < length; i++)
            {
                try
                {
                    var i1 = i;
                    tasks[i - 8] = GetViewModelByIndex(i1);
                } catch (Exception ex)
                {
                    // ignore it
                }
            }

            result = (await Task.WhenAll(tasks).ConfigureAwait(false)).Where(r => r != null).ToList();
            return result;
        }

        private async Task<AssetViewModel> GetViewModelByIndex(int index)
        {
            try
            {
                var item = await _accountsService.GetItem(index);

                var descriptionJson = await _ipfsProviderService.GetTextAsync(item.DescriptionHash);

                var model = JsonConvert.DeserializeObject<AssetViewModel>(descriptionJson);
                model.Token = item.TokenId;
                model.Link = $"https://ipfs.infura.io/ipfs/{item.FileHash}";

                return model;
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
            
        }
    }
}
