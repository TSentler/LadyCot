using Chains;
using Data;
using Hero;

namespace DefaultNamespace
{
    public class InteractAction : IChainAction
    {
        private readonly HeroBehaviour _heroBehaviour;
        private readonly InteractionPointData _data;

        public InteractAction(HeroBehaviour heroBehaviour, InteractionPointData data)
        {
            _heroBehaviour = heroBehaviour;
            _data = data;
        }
        
        public void Execute(Chain chain)
        {
            _heroBehaviour.Interact(_data);
        }
    }
}