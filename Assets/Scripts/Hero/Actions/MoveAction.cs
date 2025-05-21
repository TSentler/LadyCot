using Chains;
using UnityEngine;

namespace Hero
{
    public class MoveAction : IChainAction
    {
        private readonly HeroBehaviour _heroBehaviour;
        private readonly Vector3 _destination;

        public MoveAction(HeroBehaviour heroBehaviour, Vector3 destination)
        {
            _heroBehaviour = heroBehaviour;
            _destination = destination;
        }
        
        public void Execute(Chain chain)
        {
            _heroBehaviour.MoveToPoint(_destination);
        }
    }
}