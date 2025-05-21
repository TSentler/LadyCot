using System;
using TMPro;
using Tool;
using UI;
using UnityEngine;

namespace Dialogs
{
    public class DialogView : MonoBehaviour
    {
        public TMP_Text NameText;
        public TMP_Text MessageText;
        public DialogButtons DialogButtons;
        public BackgroundSwitcher BackgroundSwitcher;

        private TextFXView _nameFx, _messageFx;
        
        private void Awake()
        {
            _nameFx = NameText.AddFX();
            _messageFx = MessageText.AddFX();
        }

        public void Clear()
        {
            NameText.text = "";
            MessageText.text = "";
            FXCleanup();
        }

        public void SetPhrase(Phrase phrase)
        {
            DialogButtons.InitializePhrase();
            NameText.text = phrase.Name;
            MessageText.text = phrase.Message;
            FXCleanup();
            BackgroundSwitcher.Activate(phrase.BackgroundIndex);

            if (phrase is PhraseFork)
            {
                SetPhrase(phrase as PhraseFork);
            }
        }

        private void SetPhrase(PhraseFork phraseFork)
        {
            DialogButtons.InitializePhraseFork(phraseFork);
        }

        private void FXCleanup()
        {
            _nameFx.CleanOriginalVertices();
            _messageFx.CleanOriginalVertices();
        }
    }
}