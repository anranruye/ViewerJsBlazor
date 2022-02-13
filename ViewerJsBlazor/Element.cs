using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace ViewerJsBlazor
{
    public class Element : ComponentBase
    {
        [Parameter]
        public string Type { get; set; } = "div";

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public ElementReference ElementRef { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, Type);
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddElementReferenceCapture(2, element => ElementRef = element);
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}
