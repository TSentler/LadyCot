using Chains;
using Data;
using UnityEngine;

namespace Dialogs.Actions
{
    public class DialogAction : IChainAction
    {
        private readonly Dialog _dialog;
        private Chain _currentChain;

        public DialogAction(Dialog dialog)
        {
            _dialog = dialog;
        }
        
        public void Execute(Chain chain)
        {
            _currentChain = chain;
            _dialog.OnEnded.AddListener(DialogEnded);
            _dialog.StartDialog();
        }

        private void DialogEnded()
        {
            Debug.Log("Осмотрел объект");
            _dialog.OnEnded.RemoveListener(DialogEnded);
            _currentChain.ExecuteNext();
        }
    }
}