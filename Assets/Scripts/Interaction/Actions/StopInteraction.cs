using Chains;
using Services.Interaction;
using UnityEngine;

namespace Interaction.Actions
{
    public class StopInteraction : IChainAction
    {
        private readonly IInteractionStatus _status;

        public StopInteraction(IInteractionStatus status)
        {
            _status = status;
        }
        
        public void Execute(Chain chain)
        {
            Debug.Log("Stop Interaction");
            _status.StopInteraction();
            chain.ExecuteNext();
        }
    }
}