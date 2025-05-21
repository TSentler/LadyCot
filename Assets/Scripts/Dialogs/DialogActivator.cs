using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class DialogActivator : MonoBehaviour, IActivatable
    {
        [SerializeField] private GameObject _root;

        public event UnityAction Activated, Deactivated;

        public void DeactivateNoCollback()
        {
            _root.SetActive(false);
        }

        public void Activate()
        {
            _root.SetActive(true); 
            Activated?.Invoke();
        }

        public void Deactivate()
        {
            _root.SetActive(false);
            Deactivated?.Invoke();
        }
    }
}