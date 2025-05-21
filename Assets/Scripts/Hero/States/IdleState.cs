using Infrastructure.States;
using Services.Interaction;
using UnityEngine;

namespace Hero
{
    public class IdleState : IState, IUpdatableState
    {
        private readonly IInteractionChainService _interactionChainService;
        private readonly LottiePresenter _lottiePresenter;

        public IdleState(IInteractionChainService interactionChainService, LottiePresenter lottiePresenter)
        {
            _interactionChainService = interactionChainService;
            _lottiePresenter = lottiePresenter;
        }

        public void Enter()
        {
            Debug.Log("Idle entered");
            _lottiePresenter.PlayIdle();
            _interactionChainService.ExecuteNext();
        }
        
        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}