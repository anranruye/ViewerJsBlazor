window.ViewerJsBlazor = {
    recreate: function (viewer, element, options, dotnetInstance) {
        viewer.destroy();
        return this.create(element, options, dotnetInstance);
    },
    create: function (element, options, dotnetInstance) {
        options = options || {};
        if (dotnetInstance) {
            options.ready = function () {
                dotnetInstance.invokeMethodAsync("OnReady");
            }
        }
        return new Viewer(element, options);
    }
}