using Infrastructure.States;
using Logic;
using Services.Interaction;
using UnityEngine;
using UnityEngine.AI;

namespace Hero
{
    public class AgentMoveState : IPayloadedState<Vector3>, IUpdatableState
    {
        private readonly PreloadedStateMachine _stateMachine;
        private readonly NavMeshAgent _agent;
        private readonly FlipHeroRenderer _flipHeroRenderer;
        private readonly IInteractionChainService _chainService;
        private readonly LottiePresenter _lottiePresenter;
        private readonly AgenPerception _agentPerception;

        public AgentMoveState(PreloadedStateMachine stateMachine, NavMeshAgent agent,
            AgenPerception agentPerception, FlipHeroRenderer flipHeroRenderer,
            IInteractionChainService chainService, LottiePresenter lottiePresenter)
        {
            _stateMachine = stateMachine;
            _agent = agent;
            _flipHeroRenderer = flipHeroRenderer;
            _chainService = chainService;
            _lottiePresenter = lottiePresenter;
            _agentPerception = agentPerception;
        }
        
        public void Enter(Vector3 point)
        {
            _lottiePresenter.PlayWalk();
            _agent.SetDestination(point);
        }

        public void Update()
        {
            if (_agentPerception.IsAgentReachedDestination())
            {
                Debug.Log("Agent Reached");
                _chainService.ExecuteNext();
                return;
            }
            
            if (_agent.hasPath)
            {
                _flipHeroRenderer.Flip(_agent.steeringTarget, _agent.transform.position);
            }
        }

        public void Exit()
        {
        }
    }
}