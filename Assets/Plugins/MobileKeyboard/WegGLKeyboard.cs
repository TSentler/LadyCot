using System.Runtime.InteropServices;

namespace Plugins.MobileKeyboard
{
	public static class WegGLKeyboard
	{
		[DllImport("__Internal")]
		public static extern void FocusHandleAction (string _name, string _str);
	}
}
