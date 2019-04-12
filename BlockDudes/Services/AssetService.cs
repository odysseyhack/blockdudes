using BlockDudes.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
   
    public class AssetService
    {
        private readonly ConcurrentBag<AssetViewModel> _assets = new ConcurrentBag<AssetViewModel>();

        public List<AssetViewModel> GetAll()
        {
            return _assets.ToList();
        }

        public void Add(AssetViewModel model)
        {
            _assets.Add(model);
        }
    }
}
