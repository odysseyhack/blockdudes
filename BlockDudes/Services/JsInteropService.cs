using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlockDudes.Services
{
    public class JsInteropService
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetFileData(string fileInputRef)
        {
            return (await _jsRuntime.InvokeAsync<StringHolder>("getFileData", fileInputRef)).Content;
        }
    }

    public class StringHolder
    {
        public string Content { get; set; }
    }
}
