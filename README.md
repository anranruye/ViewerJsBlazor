# ViewerJsBlazor

A blazor wrapper for Viewer.js JavaScript image viewer library.

## Installation

By Package Manager:
```
Install-Package ViewerJsBlazor -Version 1.0.0
```

By CLI:
```
dotnet add package ViewerJsBlazor --version 1.0.0
```

By PackageReference:
```
<PackageReference Include="ViewerJsBlazor" Version="1.0.0" />
```

## Usage

In your `_Host.cshtml` or `index.html` file:
```html
<link href="https://cdnjs.cloudflare.com/ajax/libs/viewerjs/1.10.4/viewer.min.css" rel="stylesheet">
<script src="https://cdnjs.cloudflare.com/ajax/libs/viewerjs/1.10.4/viewer.min.js"></script>
<script src="_content/ViewerJsBlazor/interop.js"></script>
```

In a razor page:
```html
@using ViewerJsBlazor

@*image group*@
<ViewerJsImageGroup ElementType="ul" Options="options">
    @for (var i = 1; i <= 9; i++)
    {
        <li>
            <ViewerJsImage data-original="imagePath" src="thumbnailPath" alt="alt message" />
        </li>
    }
</ViewerJsImageGroup>

@*single image*@
<ViewerJsImage data-original="imagePath" src="thumbnailPath" alt="alt message" Options="options" />

@code{
  object options = new { url = "data-original" };
}

```

##API

Todo
