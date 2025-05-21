using Chains;

namespace Hero
{
    public class IdleAction : IChainAction
    {
        private readonly HeroBehaviour _heroBehaviour;

        public IdleAction(HeroBehaviour heroBehaviour)
        {
            _heroBehaviour = heroBehaviour;
        }
        
        public void Execute(Chain chain)
        {
            _heroBehaviour.ToIdle();
        }
    }
}