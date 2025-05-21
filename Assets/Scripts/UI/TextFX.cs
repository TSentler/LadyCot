using TMPro;
using UnityEngine;

namespace UI
{
    public class TextFX
    {
        private TMP_Text _tmpText;
        private FXType _effectType;
        //RotationFX
        private int _angle = -4;
        //CrumpFX
        private float _distortion = 0.8f;
    
        private Matrix4x4 _matrixMinus;
        private Matrix4x4 _matrixPlus;
        private TMP_TextInfo _textInfo;
        private Vector3[][] _originalVertices;
        private int _rotationSign = 1;
        
        public enum FXType
        {
            Crumple,
            Tilt,
            Jitter
        }

        public TextFX(TMP_Text tmpText, FXType effectType = FXType.Crumple, int angle = -4, float distortion = 0.8f)
        {
            _tmpText = tmpText;
            _effectType = effectType;
            _angle = angle;
            _distortion = distortion;
            _matrixMinus = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -_angle), Vector3.one);
            _matrixPlus = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, _angle), Vector3.one); 
            CleanOriginalVertices();
        }

        public void Set(int angle = -4, float distortion = 0.8f)
        {
            _angle = angle;
            _distortion = distortion;
        }

        public void CleanOriginalVertices()
        {
            _originalVertices = null;
        }
    
        public void ModifyVertices()
        {
            if (_originalVertices == null || _originalVertices.Length == 0)
                CacheOriginalVertices();

            if (_textInfo.characterCount == 0) 
                return;

            _rotationSign *= -1;
            int charRotationSign = _rotationSign;
            
            for (int i = 0; i < _textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = _textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;


                int materialIndex = charInfo.materialReferenceIndex;
                int vertexIndex = charInfo.vertexIndex;

                Vector3[] sourceVerts = _originalVertices[materialIndex];
                Vector3[] destVerts = _textInfo.meshInfo[materialIndex].vertices;
            
                switch (_effectType)
                {
                    case FXType.Crumple:
                        ApplyCrumpleEffect(sourceVerts, destVerts, vertexIndex);
                        break;
                    case FXType.Jitter:
                        ApplyJitterEffect(sourceVerts, destVerts, vertexIndex);
                        break;
                    case FXType.Tilt:
                        charRotationSign *= -1;
                        ApplyTiltEffect(sourceVerts, destVerts, vertexIndex, charRotationSign);
                        break;
                }
            }

            _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }

        private void ApplyCrumpleEffect(Vector3[] sourceVerts, Vector3[] destVerts, int vertexIndex)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector2 offset2D = Random.insideUnitCircle * _distortion;
                Vector3 offset = new Vector3(offset2D.x, offset2D.y, 0);
                destVerts[vertexIndex + j] = sourceVerts[vertexIndex + j] + offset;
            }
        }

        private void ApplyTiltEffect(Vector3[] sourceVerts, Vector3[] destVerts, int vertexIndex, int charRotationSign)
        {
            Matrix4x4 matrix = (charRotationSign < 0) ? _matrixMinus : _matrixPlus;
            Vector3 charMidBaseline = (sourceVerts[vertexIndex + 0] + sourceVerts[vertexIndex + 2]) / 2;

            for (int j = 0; j < 4; j++)
            {
                Vector3 offset3 = sourceVerts[vertexIndex + j] - charMidBaseline;
                destVerts[vertexIndex + j] = matrix.MultiplyPoint3x4(offset3) + charMidBaseline;
            }
        }

        private void ApplyJitterEffect(Vector3[] sourceVerts, Vector3[] destVerts, int vertexIndex)
        {
            Vector2 offset2D = Random.insideUnitCircle * _distortion;
            Vector3 offset = new Vector3(offset2D.x, offset2D.y, 0);
        
            for (int j = 0; j < 4; j++)
            {
                destVerts[vertexIndex + j] = sourceVerts[vertexIndex + j] + offset;
            }
        }

        private void CacheOriginalVertices()
        {
            _tmpText.ForceMeshUpdate();
            _textInfo = _tmpText.textInfo;
            _originalVertices = new Vector3[_textInfo.meshInfo.Length][];

            for (int i = 0; i < _textInfo.meshInfo.Length; i++)
            {
                var meshVerts = _textInfo.meshInfo[i].vertices;
                _originalVertices[i] = new Vector3[meshVerts.Length];
                meshVerts.CopyTo(_originalVertices[i], 0);
            }
        }
    
    }
}
