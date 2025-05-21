using System;
using System.Collections.Generic;
using Data;
using Infrastructure.States;
using Logic;
using Services.Interaction;
using UnityEngine;
using UnityEngine.AI;

namespace Hero
{
  public class HeroBehaviour : MonoBehaviour
  {
    [SerializeField] private Transform _heroRenderer;
    [SerializeField] private LottiePresenter _lottiePresenter;
    
    private FlipHeroRenderer _flipHeroRenderer;
    private NavMeshAgent _agent;
    private InteractionPointData _currentInteractionPointData;
    private PreloadedStateMachine _stateMachine;
    private AgenPerception _agenPerception;
    private IInteractionChainService _chainService;

    public void Construct(NavMeshAgent agent, IInteractionChainService chainService)
    {
      _chainService = chainService;
      _agent = agent;
      _agent.updateRotation = false;
      _agent.updateUpAxis = false;
      _agent.enabled = true;

      _lottiePresenter.Construct();
      _agenPerception = new AgenPerception(_agent);
      _flipHeroRenderer = new FlipHeroRenderer(_heroRenderer);
      _stateMachine = new PreloadedStateMachine();
      Dictionary<Type, IUpdatableState> states = new ()
      {
        {typeof(IdleState), new IdleState(_chainService, _lottiePresenter)},
        {typeof(AgentMoveState), new AgentMoveState(_stateMachine, _agent, _agenPerception, 
          _flipHeroRenderer, _chainService, _lottiePresenter)},
        {typeof(InteractState), new InteractState(_chainService, _lottiePresenter)},
      };
      _stateMachine.Initialize(states); 
      _stateMachine.Enter<IdleState>();
    }

    private void Update()
    {
      if (_stateMachine == null)
        return;
      
      _stateMachine.Update();
    }

    public void ToIdle()
    {
      _stateMachine.Enter<IdleState>();
    }

    public void Interact(InteractionPointData data)
    {
      Debug.Log("Interact");
      bool faceRight = _agent.transform.position.x < data.ItemPosition.x;
      _flipHeroRenderer.Flip(faceRight);
      _stateMachine.Enter<InteractState>();
    }

    public void MoveToPoint(Vector3 point)
    {
      _stateMachine.Enter<AgentMoveState, Vector3>(point);
    }
  }
}