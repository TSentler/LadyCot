using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOutlineHover : MonoBehaviour
{
    [SerializeField] private Color _outlineColor = new Color(220f, 228f, 181f);
    [SerializeField] private float _outlineThickness = 30f;
    [SerializeField, Range(0f, 1f)] private float _alphaThreshold = 0.5f;
    
    private Material _materialInstance;
    private SpriteRenderer _renderer;
    

    public void Construct()
    {
        _renderer = GetComponent<SpriteRenderer>();
        InitializeMaterial();
        Deactivate();
    }

    private void InitializeMaterial()
    {
        _materialInstance = Instantiate(_renderer.sharedMaterial);
        _renderer.material = _materialInstance;
        _materialInstance.SetFloat("_AlphaThreshold", _alphaThreshold);
        _materialInstance.SetColor("_OutlineColor", _outlineColor);
    }

    public void Activate() => 
        _materialInstance.SetFloat("_OutlineThickness", _outlineThickness);

    public void Deactivate() => 
        _materialInstance.SetFloat("_OutlineThickness", 0f);
}