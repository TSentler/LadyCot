using TMPro;
using UI;

namespace Tool
{
    public static class TMPTextFXExtensions
    {
        public static TextFXView AddFX(this TMP_Text text)
        {
            TextFXView view = text.gameObject.AddComponent<TextFXView>();
            view.Construct(text);
            return view;
        }
    }
}