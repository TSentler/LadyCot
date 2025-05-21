using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class Dialog : MonoBehaviour
    {
        public bool ActicateByStart;
        public UnityEvent OnStarted;
        public UnityEvent OnEnded;

        private Phrase _currentPhrase; //������ �� ��������� ������� �����
        private Phrase _firstPhrase;
        private DialogActivator _dialogActivator;
        private DialogView _dialogView;
        private DialogButtons _dialogButton;
        private Camera _currentCamera;
        private bool _isCurrent;
        private bool _isClicked;

        public void Construct(DialogViewProvider provider)
        {
            _firstPhrase = GetComponent<Phrase>();
            _dialogView = provider.View; 
            _dialogActivator = provider.Activator; 
            _dialogButton = provider.Buttons;
            SubscribeToEvents();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            OnDisable();
            if (_dialogButton != null) 
                _dialogButton.OnClick += OnDialogClick;
        }

        private void OnDisable()
        {
            if (_dialogButton != null) 
                _dialogButton.OnClick -= OnDialogClick;
        }

        private void Start()
        {
            if (ActicateByStart)
            {
                StartDialog();
            }
        }

        public void StartDialog()
        {
            _isCurrent = true;
            _dialogActivator.Activate();
            _currentPhrase = _firstPhrase;
            Say();
            OnStarted.Invoke();
        }

        public void NextPhrase()
        {
            if (_isCurrent == false)
                return;

            _currentPhrase = _currentPhrase.GetNextPhrase();
            Say();
        }

        private void Say()
        {
            if (_currentPhrase != null)
            {
                _dialogView.SetPhrase(_currentPhrase);
                CameraActivate();
            }
            else
            {
                _isCurrent = false;
                _dialogActivator.Deactivate();
                CameraDeactivate();
                OnEnded.Invoke();
            }
        }

        private void CameraActivate()
        {
            if (_currentPhrase.Camera == null)
                return;

            CameraDeactivate();
            _currentCamera = _currentPhrase.Camera;
            _currentCamera.gameObject.SetActive(true);
        }

        private void CameraDeactivate()
        {
            if (_currentCamera != null)
            {
                _currentCamera.gameObject.SetActive(false);
            }
        }

        private void OnDialogClick()
        {
            NextPhrase();
        }
    }
}