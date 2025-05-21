using System;
using Logic;
using UnityEngine;

namespace Hero
{
  public class AnimationPresenter : MonoBehaviour, IAnimationStateReader
  {
    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _walkStateHash = Animator.StringToHash("Walk");
    private readonly int _interactStateHash = Animator.StringToHash("Interact");

    [SerializeField] private Animator _animator;

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }
    public bool IsInteracted => State == AnimatorState.Interact;

    public void PlayInteract()
    {
      _animator.SetTrigger(_interactStateHash);
    }

    public void ResetToIdle()
    {
      _animator.Play(_idleStateHash, -1);
    }

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash)
    {
      StateExited?.Invoke(StateFor(stateHash));
    }

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
        state = AnimatorState.Idle;
      }
      else if (stateHash == _interactStateHash)
      {
        state = AnimatorState.Interact;
      }
      else if (stateHash == _walkStateHash)
      {
        state = AnimatorState.Walk;
      }
      else
      {
        state = AnimatorState.Unknown;
      }

      return state;
    }
  }
}