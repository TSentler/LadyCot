using Data;
using Infrastructure.Factory;
using UnityEngine;

namespace Services.Interaction
{
    public interface IInteractionChainService : IService
    {
        bool IsInteraction { get; }
        void Initialise(IGameFactory gameFactory);
        void ExecuteNext();
        void ExecuteMove(Vector2 inputMousePosition);
        void ExecuteInteraction(InteractionPointData data);
    }
}