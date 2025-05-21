using System;
using System.Collections.Generic;
using Chains;
using Data;
using Infrastructure.Factory;
using Services.Input;
using Services.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using Dialogs;
using Dialogs.Actions;

namespace Interaction
{
    public class InteractionPoint : MonoBehaviour
    {
        [SerializeField] private bool _isPickable;
        [SerializeField] private string _pointName;
        [SerializeField] private WalkPoint _walkPoint;
        [SerializeField] private Dialog _dialog;
        
        private IInputService _input;
        private ContextMenuBuilder _contextMenuBuilder;
        private InteractionPanelView _interactionPanelView;
        private InteractionMouseInfoView _mouseInfoView;
        private IInteractionChainService _interactionChainService;
        private SpriteOutlineHover _outline;
        private bool _isSilentHover;

        private void OnValidate()
        {
            if (_dialog == null 
                && GetComponentInChildren<Dialog>() is {} dialog
                && dialog.gameObject.activeSelf) 
                _dialog = GetComponentInChildren<Dialog>();
        }

        public void Construct(IInputService input,
            InteractionMouseInfoView mouseInfoView,
            InteractionPanelView interactionPanelView,
            ContextMenuBuilder contextMenuBuilder,
            DialogViewProvider dialogViewProvider,
            IInteractionChainService interactionChainService)
        {
            _interactionChainService = interactionChainService;
            _mouseInfoView = mouseInfoView;
            _interactionPanelView = interactionPanelView;
            _input = input;
            _contextMenuBuilder = contextMenuBuilder;
            _dialog?.Construct(dialogViewProvider);

            if (TryGetComponent(out _outline))
                _outline.Construct();
        }

        private void OnMouseDown()
        {
            if (_interactionChainService.IsInteraction 
                || EventSystem.current.IsPointerOverGameObject())
                return;
            
            _interactionPanelView.Hide();
            var data = GetPointData();
            _contextMenuBuilder.Build(data);
            _interactionPanelView.Show(at: _input.MousePosition, this);
            HideMouseText();
        }

        private void OnMouseEnter()
        {
            if (IsInteraction())
            {
                Debug.Log("Is Silent Hover Active");
                _isSilentHover = true;
                return;
            }

            _isSilentHover = false;
            ShowMouseText();
        }

        private void OnMouseOver()
        {
            if (_isSilentHover && IsInteraction() == false)
            {
                Debug.Log("Is Silent Hover Do");
                _isSilentHover = false;
                ShowMouseText();
            }   
        }

        private void OnMouseExit()
        {
            HideMouseText();
            _isSilentHover = false;
        }

        private bool IsInteraction() =>
            _interactionChainService?.IsInteraction ?? true;
        private bool IsCurrentInteractPanelShowed() =>
            _interactionPanelView?.IsShowed(this) ?? false;

        private InteractionPointData GetPointData()
        {
            List<InteractActionData> interactActions = new List<InteractActionData>();
            if (_isPickable)
            {
                interactActions.Add(new InteractActionData {
                        InteractionType = InteractionType.Pickup,
                        ActionName = "Взять",
                    });
            }
                
            if (_dialog != null)
            {
                interactActions.Add(new InteractActionData {
                        InteractionType = InteractionType.Watch,
                        ActionName = "Осмотреть",
                        Chain = new Chain(new IChainAction[] {
                            new DialogAction(_dialog),
                        }),
                    });
            }
        
            InteractionPointData data = new InteractionPointData()
            {
                ItemPosition = transform.position,
                WalkPosition = GetWalkPointPosition(),
                InteractActions = interactActions.ToArray(),
            };
            return data;
        }

        private Vector2 GetWalkPointPosition()
        {
            if (_walkPoint == null)
                return _input.MousePosition;

            return _walkPoint.Position;
        }
        
        private void ShowMouseText()
        {
            if (IsCurrentInteractPanelShowed())
                return;
            
            _mouseInfoView.SetText(_pointName);
            _outline?.Activate();
        }

        private void HideMouseText()
        {
            _mouseInfoView.ResetText();
            _outline?.Deactivate();
        }
    }
}
