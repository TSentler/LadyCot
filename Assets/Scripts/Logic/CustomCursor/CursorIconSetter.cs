using Infrastructure.AssetManagement;
using UnityEngine;

namespace Logic.CustomCursor
{
    public class CursorIconSetter
    {
        public void SetCursor()
        {
            Texture2D cursorTexture = Resources.Load<Texture2D>(AssetPath.CursorPath);
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
