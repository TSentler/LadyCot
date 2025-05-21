using UnityEngine;

namespace Logic
{
    public class Movement: MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 2f;
        [SerializeField] private float _minTravelTime = 0.8f;
    
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _progress;
        private float _distance;
        private float _actualSpeed;
        private float _timeDestination;

        private void Awake()
        {
            _startPosition = transform.position;
            _targetPosition = transform.position;
        }

        public void StartMove(Vector2 to)
        {
            _targetPosition = to;
            _startPosition = transform.position;
            _progress = 0f;
            _distance = Vector2.Distance(_startPosition, _targetPosition);
            _actualSpeed = Mathf.Min(_movementSpeed, _distance / _minTravelTime);
        }

        public void Move()
        {
            _progress += Time.deltaTime * (_actualSpeed / _distance);
            _progress = Mathf.Clamp01(_progress);
            float easedProgress = -(Mathf.Cos(Mathf.PI * _progress) - 1f) / 2f;
            transform.position = Vector2.Lerp(_startPosition, _targetPosition, easedProgress);
        }

        public bool InRide() => 
            Vector3.Distance(transform.position, _targetPosition) > Constants.Epsilon;
    }
}