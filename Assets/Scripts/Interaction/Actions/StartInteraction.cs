using Chains;
using Services.Interaction;
using UnityEngine;

namespace Interaction.Actions
{
    public class StartInteraction : IChainAction
    {
        private readonly IInteractionStatus _status;

        public StartInteraction(IInteractionStatus status)
        {
            _status = status;
        }
        
        public void Execute(Chain chain)
        {
            Debug.Log("Start Interaction");
            _status.StartInteraction();
            chain.ExecuteNext();
        }
    }
}