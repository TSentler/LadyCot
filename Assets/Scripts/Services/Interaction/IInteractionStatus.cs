namespace Services.Interaction
{
    public interface IInteractionStatus : IService
    {
        bool IsInteraction { get; }
        void StopInteraction();
        void StartInteraction();
    }
}