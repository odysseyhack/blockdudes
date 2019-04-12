using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockDudes.Models
{
    public class AssetViewModel
    {
        public string Address { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Byte[] File { get; set; }
    }
}
