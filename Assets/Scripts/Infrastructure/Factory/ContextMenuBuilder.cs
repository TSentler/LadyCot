using System.Linq;
using Data;
using Infrastructure.AssetManagement;
using Interaction;
using Services.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Factory
{
    public class ContextMenuBuilder
    {
        private readonly IAssetProvider _assets;
        private readonly IInteractionChainService _chain;
        private readonly InteractionPanelView _interactionPanelView;
        
        private Transform InteractionPanelTransform => _interactionPanelView.transform;

        public ContextMenuBuilder(IAssetProvider assets, 
            IInteractionChainService chain, InteractionPanelView interactionPanelView)
        {
            _assets = assets;
            _chain = chain;
            _interactionPanelView = interactionPanelView;
        }

        public void Build(InteractionPointData data)
        {
            Clear();
            
            for (var i = 0; i < data.GetInteractionsCount(); i++)
            {
                InteractActionData actionData = data.GetInteraction(i);
                GameObject instantiate = _assets.Instantiate(AssetPath.InteractionButton, InteractionPanelTransform);
                InteractionButton button = instantiate.GetComponentInChildren<InteractionButton>();
                int currentIndex = i;
                UnityAction clickAction = () =>
                {
                    data.SelectAction(currentIndex);
                    _chain.ExecuteInteraction(data);
                };

                button.Construct(actionData.ActionName, clickAction);
            }
        }

        private void Clear()
        {
            InteractionPanelTransform.GetComponentsInChildren<InteractionButton>().ToList()
                .ForEach(i => Object.Destroy(i.gameObject));
        }
    }
}