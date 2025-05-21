using Chains;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public enum InteractionType
    {
        Pickup,
        Watch
    }
    
    public class InteractActionData
    {
        public string ActionName;
        public InteractionType InteractionType;
        public Chain Chain;
    }
    
    public class InteractionPointData
    {
        public Vector2 ItemPosition;
        public Vector2 WalkPosition;
        public int SelectedAction;
        public InteractActionData[] InteractActions;
        
        public void SelectAction(int i) => 
            SelectedAction = i;
        
        public Chain SelectedChain() => 
            InteractActions[SelectedAction]?.Chain;

        public InteractActionData GetInteraction(int i) => 
            InteractActions[i];
        
        public int GetInteractionsCount() => 
            InteractActions.Length;

        public InteractActionData GetSelectedInteraction()
        {
            return GetInteraction(SelectedAction);
        }
    }
}