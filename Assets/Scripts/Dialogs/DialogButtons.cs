using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class DialogButtons : MonoBehaviour
    {
        private bool _isClicked;
        private PhraseFork _phraseFork;
        public GameObject ForkPanel;
        public TMP_Text ForkTextA;
        public TMP_Text ForkTextB;

        public event UnityAction OnClick;

        private bool IsFork => _phraseFork != null;

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && _isClicked == false)
            {
                Click();
            }
        }

        private void LateUpdate()
        {
            _isClicked = false;
        }

        public void InitializePhrase()
        {
            _phraseFork = null;
            ForkPanel.SetActive(false);
        }

        public void InitializePhraseFork(PhraseFork phraseFork)
        {
            _phraseFork = phraseFork;
            ForkTextA.text = phraseFork.ButtonAText;
            ForkTextB.text = phraseFork.ButtonBText;
            ForkPanel.SetActive(true);
        }

        public void Click()
        {
            if (IsFork)
                return;
                
            _isClicked = true;
            OnClick?.Invoke();
        }

        public void ClickForkA()
        {
            _phraseFork.ForkA();
            OnClick?.Invoke();
        }

        public void ClickForkB()
        {
            _phraseFork.ForkB();
            OnClick?.Invoke();
        }
    }
}