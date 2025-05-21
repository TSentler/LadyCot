using SkiaSharp.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Hero
{
    public class LottiePresenter : MonoBehaviour
    {
        private readonly string _idleStateName = "Idle";
        private readonly string _walkStateName = "Walk"; 
        private readonly string _interactStateName = "Take";
        
        [SerializeField] private string _path = "json/";
        [SerializeField] private SkottiePlayerV2 _skottiePlayer;
    
        public event UnityAction InteractEnd;
        
        public void Construct()
        {
            _skottiePlayer.OnAnimationFinished += AnimationFinished;
            TextAsset json = Resources.Load<TextAsset>(_path);
            Debug.Log(json);
            _skottiePlayer.LoadAnimation(json.text);
            Debug.Log("Loaded animation");
            PlayIdle();
            Debug.Log("Playing animation");
            _skottiePlayer.PlayAnimation();
        }

        public void PlayIdle() => 
            SetState(_idleStateName);

        public void PlayWalk() => 
            SetState(_walkStateName);

        public void PlayInteract()
        {
            SetState(_interactStateName);
        }

        private void SetState(string state)
        {
            _skottiePlayer.SetState(state);
            _skottiePlayer.PlayAnimation();
        }

        private void AnimationFinished(string state)
        {
            if (state == _interactStateName)
            {
                InteractEnd?.Invoke();
            }
        }

    }
}