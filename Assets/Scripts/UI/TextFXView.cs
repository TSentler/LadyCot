using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextFXView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private float _interval = 0.4f;
        //RotationFX
        //[SerializeField]
        private int _angle = -4;
        //CrumpFX
        [SerializeField] public float _distortion = 0.8f;

        private TextFX _textFX;
        private float _timer;

        private void OnValidate()
        {
            if (_tmpText == null)
                _tmpText = GetComponent<TMP_Text>();
        }

        public void Construct(TMP_Text tmpText, TextFX.FXType effectType = TextFX.FXType.Crumple, int angle = -4, float distortion = 0.8f)
        {
            _tmpText = tmpText;
            _angle = angle;
            _distortion = distortion;
            _textFX = new TextFX(_tmpText, effectType, _angle, _distortion);
        }
        
        private void Awake()
        {
            if (_textFX == null && _tmpText != null)
                Construct(_tmpText, TextFX.FXType.Crumple, _angle, _distortion);
        }

        private void LateUpdate()
        {
            if (_textFX == null) 
                return;
            
#if UNITY_EDITOR
            _textFX.Set(_angle, _distortion);
#endif
        
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                _timer = 0f;
                _textFX.ModifyVertices();
            }
        }
        
        public void CleanOriginalVertices() => 
            _textFX.CleanOriginalVertices();
    }
}
