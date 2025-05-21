using Chains;
using Data;
using DefaultNamespace;
using Dialogs.Actions;
using Hero;
using Infrastructure.Factory;
using Interaction.Actions;
using UnityEngine;

namespace Services.Interaction
{
    public class InteractionChainService : IInteractionChainService
    {
        private readonly IInteractionStatus _status;
        private IGameFactory _gameFactory;
        private Chain _chain;

        public bool IsInteraction => _status.IsInteraction;
        
        public InteractionChainService(IInteractionStatus status)
        {
            _status = status;
        }
        
        public void Initialise(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void ExecuteNext()
        {
          _chain?.ExecuteNext();
        }

        public void ExecuteMove(Vector2 position)
        {
            Debug.Log("Executing move");
            _gameFactory.InteractionPanelView.Hide();
            Chain chain = new Chain(new IChainAction[]
            {
                new MoveAction(_gameFactory.HeroBehaviour, position),
                new IdleAction(_gameFactory.HeroBehaviour),
            });
            TryExecute(chain);
        }

        public void ExecuteInteraction(InteractionPointData data)
        {
            _gameFactory.InteractionPanelView.Hide();
            InteractActionData actionData = data.GetSelectedInteraction();
            switch (actionData.InteractionType)
            {
                case InteractionType.Pickup:
                    ExecuteGetInteraction(data);
                    break;
                case InteractionType.Watch:
                    ExecuteWatchInteraction(data);
                    break;
            }
        }

        private void TryExecute(Chain chain)
        {
            if (_status.IsInteraction)
                Debug.LogWarning("Trying to execute processed chain");
            
            Execute(chain);
        }

        private void Execute(Chain chain)
        {
            _chain = chain;
            ExecuteNext();
        }

        private void ExecuteGetInteraction(InteractionPointData data)
        {
            Debug.Log("Executing get interaction");
            Chain chain = new Chain(new IChainAction[]
            {
                new StartInteraction(_status),
                new MoveAction(_gameFactory.HeroBehaviour, data.WalkPosition),
                new InteractAction(_gameFactory.HeroBehaviour, data),
                new IdleAction(_gameFactory.HeroBehaviour),
            });

            Chain interactionPointChain = data.SelectedChain();
            if (interactionPointChain != null)
                chain.AddAction(interactionPointChain);
            
            chain.AddAction(new StopInteraction(_status));
            
            Execute(chain);
        }

        private void ExecuteWatchInteraction(InteractionPointData data)
        {
            
            Debug.Log("Executing watch interaction");
            Chain chain = new Chain(new IChainAction[]
            {
                new StartInteraction(_status),
                new IdleAction(_gameFactory.HeroBehaviour),
            });
            
            Chain interactionPointChain = data.SelectedChain();
            if (interactionPointChain != null)
                chain.AddAction(interactionPointChain);
            
            chain.AddAction(new StopInteraction(_status));
            Execute(chain);
        }
    }
}