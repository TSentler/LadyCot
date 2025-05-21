using UnityEngine.AI;

namespace Logic
{
    public class AgenPerception
    {
        private readonly NavMeshAgent _agent;

        public AgenPerception(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public bool IsAgentReachedDestination() =>
            _agent.pathPending == false
            && _agent.remainingDistance <= _agent.stoppingDistance
            && (_agent.hasPath == false || _agent.velocity.sqrMagnitude == 0f);
    }
}