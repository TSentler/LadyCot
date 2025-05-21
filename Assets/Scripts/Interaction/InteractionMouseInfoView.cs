using System;
using TMPro;
using Tool;
using UI;
using UnityEngine;

namespace Interaction
{
    public class InteractionMouseInfoView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _text;
        
        private TextFXView _textFX;

        private void Awake()
        {
            _textFX = _text.AddFX();
        }
        
        public void SetText(string text)
        {
            _text.text = text;
            _textFX.CleanOriginalVertices();
            _root.SetActive(true);
        }

        public void ResetText()
        {
            _root.SetActive(false);
            _text.text = "";
            _textFX.CleanOriginalVertices();
        }
    }
}
