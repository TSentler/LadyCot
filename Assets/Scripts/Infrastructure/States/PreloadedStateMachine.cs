using System;
using System.Collections.Generic;

namespace Infrastructure.States
{
    public class PreloadedStateMachine
    {
        private Dictionary<Type, IUpdatableState> _states;
        private IUpdatableState _activeState;

        public void Initialize(Dictionary<Type, IUpdatableState> states)
        {
            _states = new Dictionary<Type, IUpdatableState>(states);
        }

        public void Update()
        {
            _activeState?.Update();
        }
        
        public void Enter<TState>() 
            where TState : class, IState, IUpdatableState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) 
            where TState : class, IPayloadedState<TPayload>, IUpdatableState
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IUpdatableState
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