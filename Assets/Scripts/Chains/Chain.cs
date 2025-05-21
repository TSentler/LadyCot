using System.Collections.Generic;

namespace Chains
{
    public class Chain
    {
        private readonly LinkedList<IChainAction> _actions;
        private IChainAction _currentAction;

        public Chain(IEnumerable<IChainAction> actions)
        {
            _actions = new LinkedList<IChainAction>(actions);
        }

        public void ExecuteNext()
        {
            if (_actions.Count == 0) return;
        
            _currentAction = _actions.First.Value;
            _actions.RemoveFirst();
            _currentAction.Execute(this);
        }

        public void AddAction(IChainAction action) => 
            _actions.AddLast(action);
        
        public void AddAction(IEnumerable<IChainAction> actions)
        {
            foreach (var action in actions) _actions.AddLast(action);
        }

        public void AddAction(Chain chain) => 
            AddAction(chain._actions);

        public int RemainingActions => _actions.Count;
    }
}
