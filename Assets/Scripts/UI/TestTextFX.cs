using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class TestTextFX : MonoBehaviour
{
    public bool IsTest;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private int _angle = -4;
    [SerializeField] private float _interval = 0.4f;
    
    public string _pattern;
    public string _originalText;
    private float _timer = 0f;
    private Matrix4x4 _matrixMinus;
    private Matrix4x4 _matrixPlus;
    private CanvasRenderer _canvasRenderer;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(_originalText))
            _originalText = _tmpText.text;
            
        if (_tmpText != null && IsTest)
        {
            _pattern = WrapEachCharInRotateTag(_originalText);
            _tmpText.text = Format(_angle);
            _tmpText.richText = true;
            IsTest = false;
        }
    }

    private void Awake()
    {
        _originalText = _tmpText.text;
        _pattern = WrapEachCharInRotateTag(_originalText);
        _matrixMinus = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, -_angle), Vector3.one);
        _matrixPlus = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, _angle), Vector3.one); 
        _canvasRenderer = GetComponent<CanvasRenderer>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        //if (_timer >= _interval)
        {
            _timer = 0f;
            _angle *= -1;
            Profiler.BeginSample("TextFX1");
            //ChangeTextWithoutRebuild(Format(_angle));
            //_tmpText.text = Format(_angle);
            Profiler.EndSample();
            Profiler.BeginSample("TextFX2");
            ModifyVertices();
            Profiler.EndSample();
        }
    }
    
    public void ChangeTextWithoutRebuild(string newText)
    {
        _tmpText.SetText(newText, true); // true - skip rich text parsing
        
        _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }
    
    private string Format(int angle)
    {
        int a = angle, b = -angle;
        string modifiedText = string.Format(_pattern, a, b);
        return modifiedText;
    }

    private string Format2(float angle)
    {
        string reg = @"<rotate=\{(\d+)\}>";
        string replacedText = Regex.Replace(_pattern, reg, match =>
        {
            angle *= -1;
            return $"<rotate={angle}>";
        });
        return replacedText;
    }

    private static string WrapEachCharInRotateTag(string text)
    {
        int i = 0;
        string result = Regex.Replace(text, @"([^\s])", match =>
        {
            string rotated = $"<rotate={{{i}}}>{match.Value}</rotate>";
            i = 1 - i;
            //i++;
            return rotated;
        });
        return result;
    } 
    
    public void ModifyVertices()
    {
        TMP_TextInfo textInfo = _tmpText.textInfo;
        
        if (textInfo.characterCount == 0) return;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            _angle *= -1;
            
            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;
            Vector3 charMidBaseline = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;

            Matrix4x4 matrix = _angle > 0 ? _matrixMinus : _matrixPlus;

            for (int j = 0; j < 4; j++)
            {
                Vector3 offset = vertices[vertexIndex + j] - charMidBaseline;
                vertices[vertexIndex + j] = matrix.MultiplyPoint3x4(offset) + charMidBaseline;
            }
        }
        _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
    
    private void ModifyVertices2()
    {
        _tmpText.ForceMeshUpdate();

        TMP_TextInfo textInfo = _tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            _angle *= -1;
            
            int vertexIndex = charInfo.vertexIndex;
            int materialIndex = charInfo.materialReferenceIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            // Центр символа
            Vector3 charMidBaseline = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;

            // Смещение к центру, поворот, возвращение
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, _angle), Vector3.one);

            for (int j = 0; j < 4; j++)
            {
                Vector3 offset = vertices[vertexIndex + j] - charMidBaseline;
                vertices[vertexIndex + j] = matrix.MultiplyPoint3x4(offset) + charMidBaseline;
            }
        }

        // Применение изменений
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            _tmpText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
