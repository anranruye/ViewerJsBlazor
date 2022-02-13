window.ViewerJsBlazor = {
    create: function (element, options) {
        var viewer;
        options = options || {};
        viewer = new Viewer(element, options);
        return DotNet.createJSObjectReference(viewer);
    },
    destroy: function (viewer) {
        viewer.destroy();
        DotNet.disposeJSObjectReference(viewer);
    },
    invokeMethod: function (viewer, methodName, params) {
        params = params || [];
        viewer[methodName].apply(viewer, params);
    }
}