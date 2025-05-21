mergeInto(LibraryManager.library, {
    SetFullScreen: function () {
        myGameInstance.SetFullscreen(1);
    },
    ExitFullScreen: function () {
        myGameInstance.SetFullscreen(0);
    }
});