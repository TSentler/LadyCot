using Interfaces;
using UnityEngine;

namespace Dialogs
{
    public class DialogActivationHandler : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _activatableBehaviour;

        private IActivatable _activatable;
        private DialogActivator _dialogActivator;

        private void OnValidate()
        {
            if (_activatableBehaviour
                && !(_activatableBehaviour is IActivatable))
            {
                Debug.LogError(nameof(_activatableBehaviour) + " needs to implement " + nameof(IActivatable));
                _activatableBehaviour = null;
            }
        }

        private void Awake()
        {
            _activatable = (IActivatable)_activatableBehaviour;
            _dialogActivator = FindObjectOfType<DialogActivator>();

        }
        
        private void OnEnable()
        {
            _dialogActivator.Activated += _activatable.Activate;
            _dialogActivator.Deactivated += _activatable.Deactivate;
        }

        private void OnDisable()
        {
            _dialogActivator.Activated -= _activatable.Activate;
            _dialogActivator.Deactivated -= _activatable.Deactivate;
        }
    }
}