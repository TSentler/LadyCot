using Dialogs;
using Infrastructure.Factory;
using Services.Input;
using Services.Interaction;
using UnityEngine;

namespace Interaction
{
    public class InteractionPointsGroup : MonoBehaviour
    {
        [SerializeField] private InteractionPoint[] _interactionPoints;

        private void OnValidate()
        {
            if (_interactionPoints.Length == 0) 
                _interactionPoints = GetComponentsInChildren<InteractionPoint>();
        }

        public void Construct(IInputService input,
            InteractionMouseInfoView mouseInfoView,
            InteractionPanelView interactionPanelView,
            ContextMenuBuilder contextMenuBuilder,
            DialogViewProvider dialogViewProvider,
            IInteractionChainService interactionChainService)
        {
            foreach (InteractionPoint point in _interactionPoints)
            {
                point.Construct(input, mouseInfoView, interactionPanelView, contextMenuBuilder, dialogViewProvider, interactionChainService);
            }
        }
    }
}