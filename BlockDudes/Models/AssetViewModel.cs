using Newtonsoft.Json;
using System;

namespace BlockDudes.Models
{
    public class AssetViewModel
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime UploadTime { get; set; }

        [JsonIgnore]
        public string Link { get; set; }
    }
}
