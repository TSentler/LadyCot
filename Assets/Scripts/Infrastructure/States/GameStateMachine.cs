﻿using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Logic;
using Services;
using Services.Input;
using Services.Interaction;

namespace Infrastructure.States
{
  public class GameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, 
          services.Single<IGameFactory>(), services.Single<IInputService>(),
          services.Single<IInteractionChainService>()),
        //[typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
        [typeof(GameLoopState)] = new GameLoopState(this),
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}