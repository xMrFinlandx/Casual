using PathCreation;
using Player.Controls;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    { 
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationOffset;
        [SerializeField] private float _limitValue;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private Animator _animator;

        private VertexPath _path;
        
        private float _cashedHorizontalInput;
        private float _travelledDistance;
        private float _currentOffsetX;

        private bool _isGameStopped;
        private bool _isPaused = true;
        private bool _canMove;

        [Inject]
        private void Construct(PathCreator pathCreator)
        {
            _path = pathCreator.path;
        }

        private void Start()
        {
            _inputReader.MouseEventPerfomed += OnMousePerfomed;
            _inputReader.MouseCancelledEvent += OnMouseCancelled;
            
            _animator.Play("idle");
        }

        private void OnMouseCancelled()
        {
            _canMove = false;
        }

        private void OnMousePerfomed()
        {
            _canMove = true;
            
            _isPaused = false;
            _animator.Play("walk");
        }

        private void Update()
        {
            if (_isGameStopped || _isPaused)
                return;
            
            FollowPath();

            if (!_canMove)
                return;

            UpdateHorizontalInput();
            UpdateXOffset();
        }

        private void UpdateHorizontalInput()
        {
            var horizontalInput = _inputReader.MousePosition.x;

            if (horizontalInput != 0)
                _cashedHorizontalInput = horizontalInput;
        }

        private void FollowPath()
        {
            _travelledDistance += _speed * Time.deltaTime;
            transform.position = _path.GetPointAtDistance(_travelledDistance, EndOfPathInstruction.Stop);
            transform.rotation = _path.GetRotationAtDistance(_travelledDistance, EndOfPathInstruction.Stop);
            transform.eulerAngles += new Vector3(0, 0, _rotationOffset);
            
            var worldOffset = transform.TransformDirection(new Vector3(_currentOffsetX, 0, 0));
            transform.position += worldOffset;
        }

        private void UpdateXOffset()
        {
            var halfScreen = Screen.width / 2;
            var x = Mathf.Clamp((_cashedHorizontalInput - halfScreen) / halfScreen * _limitValue, -_limitValue, _limitValue);
            
            _currentOffsetX = x;
        }
        
        private void OnDisable()
        {
            _inputReader.MouseEventPerfomed -= OnMousePerfomed;
            _inputReader.MouseCancelledEvent -= OnMouseCancelled;
        }

        public void Stop()
        {
            _isGameStopped = true;
        }
    }
}
