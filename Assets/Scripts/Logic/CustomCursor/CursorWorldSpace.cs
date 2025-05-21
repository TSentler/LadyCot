using UnityEngine;

namespace Logic.CustomCursor
{
    [RequireComponent(typeof(RectTransform))]
    public class CursorWorldSpace : MonoBehaviour
    {
        [SerializeField] private Vector2 _hotSpotOffset = Vector2.zero;
        [SerializeField] private float _distanceFromCamera = 0f; 
   
        private Camera _mainCamera;
        private RectTransform _customCursor;
        
        private void Awake()
        {
            _customCursor = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Cursor.visible = false;
            Vector3 screenMousePos = Input.mousePosition;
        
            if (_mainCamera.orthographic)
            {
                Vector3 worldMousePos = _mainCamera.ScreenToWorldPoint(
                    new Vector3(screenMousePos.x, 
                        screenMousePos.y, 
                        _mainCamera.nearClipPlane + _distanceFromCamera)
                );
                _customCursor.position = worldMousePos + (Vector3)_hotSpotOffset;
            }
            else
            {
                Ray ray = _mainCamera.ScreenPointToRay(screenMousePos);
                Vector3 worldMousePos = ray.GetPoint(_distanceFromCamera);
                _customCursor.position = worldMousePos + (Vector3)_hotSpotOffset;
            }
        }
    }
}