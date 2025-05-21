using UnityEngine;

namespace Logic.CustomCursor
{
    [RequireComponent(typeof(RectTransform))]
    public class CursorScreenSpace : MonoBehaviour
    {
        [SerializeField] private Vector2 _hotSpotOffset = Vector2.zero;
        [SerializeField] private float _distanceFromCamera = 0f; 
   
        private RectTransform _customCursor;
        
        private void Awake()
        {
            _customCursor = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Cursor.visible = false;
            Vector3 screenMousePos = Input.mousePosition;
            _customCursor.position = screenMousePos;
        }
    }
}