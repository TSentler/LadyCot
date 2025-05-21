using TMPro;
using Tool;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Interaction
{
    public class InteractionButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        
        private TextFXView _textFX;

        public void Construct(string actionName, UnityAction action)
        {
            _text.text = actionName;
            _textFX = _text.AddFX();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(action);
        }
    }
}