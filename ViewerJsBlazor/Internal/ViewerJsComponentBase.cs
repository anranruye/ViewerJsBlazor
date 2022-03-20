using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ViewerJsBlazor.Internal
{
    public class ViewerJsComponentBase : ComponentBase, IAsyncDisposable
    {
        private IJSObjectReference _jsInstance;
        private bool _optionsChanged = true;

        private bool _button;
        private bool _hasButtonAttribute;
        private string _url;
        private bool _hasUrlAttribute;

        private DotNetObjectReference<ViewerJsComponentBase> _dotnetInstance;

        protected virtual ElementReference ElementReference { get; }

        [Inject]
        private IJSRuntime Js { get; set; }

        [Parameter]
        public bool Button
        {
            get => _button;
            set
            {
                _hasButtonAttribute = true;
                if (_button != value)
                {
                    _optionsChanged = true;
                    _button = value;
                }
            }
        }

        [Parameter]
        public string Url
        {
            get => _url;
            set
            {
                _hasUrlAttribute = true;
                if (_url != value)
                {
                    _optionsChanged = true;
                    _url = value;
                }
            }
        }

        [Parameter]
        public EventCallback Ready { get; set; }

        [JSInvokable]
        public void OnReady()
        {
            Ready.InvokeAsync();
        }

        [Parameter]
        public IDictionary<string, object> AdditionalOptions { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        private async Task CreateJsInstance()
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            if (_hasButtonAttribute)
            {
                options["button"] = _button;
            }
            if (_hasUrlAttribute)
            {
                options["url"] = _url;
            }
            if (AdditionalOptions?.Any() == true)
            {
                foreach (KeyValuePair<string, object> item in AdditionalOptions)
                {
                    options[item.Key.ToCamelCase()] = item.Value;
                }
            }
            _dotnetInstance ??= DotNetObjectReference.Create(this);
            if (_jsInstance != null)
            {
                var newJsInstance = await Js.InvokeAsync<IJSObjectReference>(
                    "window.ViewerJsBlazor.recreate", _jsInstance, ElementReference, options, _dotnetInstance);
                await _jsInstance.DisposeAsync();
                _jsInstance = newJsInstance;
            }
            else
            {
                _jsInstance = await Js.InvokeAsync<IJSObjectReference>(
                    "window.ViewerJsBlazor.create", ElementReference, options, _dotnetInstance);
            }
        }

        private async Task DestroyJsInstance()
        {
            if (_jsInstance != null)
            {
                await _jsInstance.InvokeVoidAsync("destroy");
                await _jsInstance.DisposeAsync();
            }
        }

        public async Task InvokeMethod(string methodName, params object[] paramters)
        {
            if (_jsInstance != null)
            {
                await _jsInstance.InvokeVoidAsync(methodName, paramters);
            }
        }

        public virtual async ValueTask DisposeAsync()
        {
            await DestroyJsInstance();
            _dotnetInstance?.Dispose();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_optionsChanged)
            {
                _optionsChanged = false;
                await CreateJsInstance();
            }
        }

        public void Refresh()
        {
            _optionsChanged = true;
        }
    }
}
