namespace Services.Interaction
{
    public class InteractionStatus : IInteractionStatus
    {
        private bool _isInteraction;
        
        public bool IsInteraction => _isInteraction;
        
        public void StopInteraction()
        {
            _isInteraction = false;
        }

        public void StartInteraction()
        {
            _isInteraction = true;
        }
        
    }
}