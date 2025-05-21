using Infrastructure.States;
using Services.Interaction;
using UnityEngine;

namespace Hero
{
    public class InteractState : IState, IUpdatableState
    {
        private readonly IInteractionChainService _interactionChainService;
        private readonly LottiePresenter _lottiePresenter;

        public InteractState(IInteractionChainService interactionChainService, LottiePresenter lottiePresenter)
        {
            _interactionChainService = interactionChainService;
            _lottiePresenter = lottiePresenter;
            _lottiePresenter.InteractEnd += AnimationCompleted;
        }

        public void Enter()
        {
            Debug.Log("Interact entered");
            _lottiePresenter.PlayInteract();
        }
    
        public void Update()
        {
        }

        public void Exit()
        {
        }

        private void AnimationCompleted()
        {
            _interactionChainService.ExecuteNext();
        }
    }
}