using UnityEngine;

namespace Interaction
{
    public class InteractionPanelView : MonoBehaviour
    {
        private InteractionPoint _lastInteractionPointId;

        public bool IsShowed(InteractionPoint point) => 
            gameObject.activeSelf && point.Equals(_lastInteractionPointId);

        public void Show(Vector2 at, InteractionPoint interactionPointId)
        {
            transform.position = new Vector3(at.x, at.y, transform.position.z);
            _lastInteractionPointId = interactionPointId;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
