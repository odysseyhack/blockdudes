using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockDudes.Models
{
    public class AssetModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile FormFile { get; set; }
    }
}
