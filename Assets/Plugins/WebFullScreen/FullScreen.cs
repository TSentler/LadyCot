using System.Runtime.InteropServices;

namespace Plugins.WebFullScreen
{
    public static class FullScreen
    {
        [DllImport("__Internal")]
        public static extern void SetFullScreen();

        [DllImport("__Internal")]
        public static extern void ExitFullScreen();
    }
}