mergeInto(LibraryManager.library, {
    IsMobile: function () {
        return isMobileDevice;    
    },
    
    IsIOSDevice: function () {
        return isIOSDevice;
    }
});