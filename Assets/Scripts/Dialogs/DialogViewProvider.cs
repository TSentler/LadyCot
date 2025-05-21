using System.Reflection.Emit;
using UnityEngine;

namespace Dialogs
{
    public class DialogViewProvider : MonoBehaviour
    {
        [SerializeField] private DialogActivator _dialogActivator;
        [SerializeField] private DialogView _dialogView;
        [SerializeField] private DialogButtons _dialogButtons;
        [SerializeField] private BackgroundSwitcher _backgroundSwitcher;

        public DialogActivator Activator => _dialogActivator;
        public DialogView View => _dialogView;
        public DialogButtons Buttons => _dialogButtons;
        public BackgroundSwitcher BackSwitcher => _backgroundSwitcher;

        public void Initialize()
        {
          View.Clear();
          Activator.DeactivateNoCollback();
        }
    }
}
