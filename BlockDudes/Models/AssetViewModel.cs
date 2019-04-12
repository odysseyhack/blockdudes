using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockDudes.Models
{
    public class AssetViewModel
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public AssetImage Image { get; set; }
    }
}
