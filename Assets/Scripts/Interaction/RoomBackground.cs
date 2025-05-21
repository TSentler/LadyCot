using Chains;
using Hero;
using Services.Input;
using Services.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class RoomBackground : MonoBehaviour
    {
        private IInputService _input;
        private IInteractionChainService _chain;

        public void Construct(IInputService input, IInteractionChainService cain)
        {
            _chain = cain;
            _input = input;
        }
        
        private void OnMouseDown()
        {
            if (_chain.IsInteraction || EventSystem.current.IsPointerOverGameObject())
                return;
            
            _chain.ExecuteMove(_input.MousePosition);
        }
    }

}
