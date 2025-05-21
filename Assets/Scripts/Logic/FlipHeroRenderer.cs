using UnityEngine;

namespace Logic
{
    public class FlipHeroRenderer 
    {
        private readonly Transform transform;
        
        private float _lastFlipTime;
        private float _flipCooldown = 0.5f;
        
        public bool IsFacingRight
        {
            get => transform.localScale.x > 0;
            private set
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(transform.localScale.x) * (value ? 1 : -1);
                transform.localScale = scale;
            }
        }

        public FlipHeroRenderer(Transform transform)
        {
            this.transform = transform;
        }
        
        public void Flip(Vector3 nextPosition, Vector3 currentPosition)
        {
          bool isFacingRight = nextPosition.x > currentPosition.x;
          if (isFacingRight != IsFacingRight 
              && IsFlipCooldown() == false)
          {
            Flip(isFacingRight);
            _lastFlipTime = Time.time;
          }
        }

        public void Flip(bool faceRight)
        {
            if (faceRight == IsFacingRight) 
                return;
        
            IsFacingRight = faceRight;
        }

        private bool IsFlipCooldown() => 
          Time.time - _lastFlipTime < _flipCooldown;
        
    }
}