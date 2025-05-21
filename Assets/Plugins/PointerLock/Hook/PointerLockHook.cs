#if !UNITY_EDITOR && UNITY_WEBGL
#define WEBGL_DEFINE
using Plugins.PointerLock;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace PointerLock.Hook
{
    public class PointerLockHook : MonoBehaviour
    {
        private const string CorrectName = "JavascriptHook";

        [Header("GameObject name must be " + CorrectName)]
        public bool IsNameCorrect;

        public event UnityAction PointerLocked, PointerUnlocked;

        private void OnValidate()
        {
            if (gameObject.name == CorrectName && IsNameCorrect)
                return;

            if (gameObject.name != CorrectName && IsNameCorrect)
            {
                gameObject.name = CorrectName;
            }
            if (gameObject.name == CorrectName && IsNameCorrect == false)
            {
                IsNameCorrect = true;
            }
        }

        private void Awake()
        {
#if WEBGL_DEFINE
            PointerLockListener.PointerLockListenerInitialize();
#endif
        }

        private void OnEnable()
        {
#if WEBGL_DEFINE
            PointerLockListener.AddPointerLockListener();
#endif
        }

        private void OnDisable()
        {
#if WEBGL_DEFINE
            PointerLockListener.RemovePointerLockListener();
#endif
        }

        public void OnLockCursorChanged(int isLock)
        {
            if (isLock > 0)
            {
                PointerLocked?.Invoke();
            }
            else
            {
                // Cursor.lockState = CursorLockMode.None;
                // Application.ExternalEval("window.focus();");
                PointerUnlocked?.Invoke();
            }
        }
    }
}
