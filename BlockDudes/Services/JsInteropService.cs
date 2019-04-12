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

        public async Task Alert(string text)
        {
            await _jsRuntime.InvokeAsync<object>("alert", text);
        }
    }
}
