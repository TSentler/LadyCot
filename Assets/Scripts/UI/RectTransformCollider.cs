using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class RectTransformCollider : MonoBehaviour
    {
        [SerializeField] private bool _isConstrain;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private BoxCollider2D _collider;

        private void Update()
        {
            UpdateEnabled();
            UpdateColliderSize();
            Constrain();
        }

        private void UpdateEnabled() => 
            _collider.enabled = _rectTransform.gameObject.activeSelf;

        private void Constrain()
        {
            if (_isConstrain == false)
                return;
            
            Transform transform = _collider.transform;
            Transform parent = transform.parent;
            float z = transform.localPosition.z;
            transform.SetParent(_rectTransform);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            transform.SetParent(parent);
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, z);

            if (_collider.transform is RectTransform rectTransform)
            {
                rectTransform.anchoredPosition = _rectTransform.anchoredPosition;
                rectTransform.sizeDelta = _rectTransform.sizeDelta;
                rectTransform.pivot = rectTransform.pivot;
            }
        }

        private void UpdateColliderSize()
        {
            Vector2 size = new Vector2(
                _rectTransform.rect.width,
                _rectTransform.rect.height
            );

            if (size.x <= 0 || size.y <= 0)
            {
                size = _rectTransform.sizeDelta;
            }
            
            _collider.size = size;
            
            Vector2 pivot = _rectTransform.pivot;
            _collider.offset = new Vector2(
                (0.5f - pivot.x) * size.x,
                (0.5f - pivot.y) * size.y
            );
        }
        
    }
}
