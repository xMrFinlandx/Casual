using PathCreation;
using Player.Controls;
using UnityEngine;
using Utilities.FSM;

namespace Player.States
{
    public class WalkState : FsmState
    {
        private readonly InputReader _inputReader;
        private readonly Animator _animator;
        private readonly Transform _transform;
        private readonly VertexPath _path;
        
        private readonly float _limitValue;
        private readonly float _speed;
        private readonly float _rotationOffset;
        
        private readonly int _hash;

        private bool _canMove;

        private float _cashedHorizontalInput;
        private float _currentOffsetX;
        private float _travelledDistance;
        
        public WalkState(FiniteStateMachine finiteStateMachine, Transform transform, VertexPath path, Animator animator, InputReader inputReader, float speed, float rotationOffset, float limitValue, int hash) : base(finiteStateMachine)
        {
            _animator = animator;
            _transform = transform;
            _path = path;
            _speed = speed;
            _rotationOffset = rotationOffset;
            _hash = hash;
            _limitValue = limitValue;
            _inputReader = inputReader;
        }

        public override void Enter()
        {
            _animator.Play(_hash);
            _inputReader.MousePerfomedEvent += OnMousePerfomed;
            _inputReader.MouseCancelledEvent += OnMouseCancelled;
        }

        public override void Exit()
        {
            _inputReader.MousePerfomedEvent -= OnMousePerfomed;
            _inputReader.MouseCancelledEvent -= OnMouseCancelled;
        }
        
        public override void Update()
        {
            FollowPath();

            if (!_canMove)
                return;
            
            UpdateHorizontalInput();
            UpdateXOffset();
        }

        private void OnMouseCancelled()
        {
            _canMove = false;
        }

        private void OnMousePerfomed()
        {
            _canMove = true;
        }
        
        private void UpdateXOffset()
        {
            var halfScreen = Screen.width / 2;
            var x = Mathf.Clamp((_cashedHorizontalInput - halfScreen) / halfScreen * _limitValue, -_limitValue, _limitValue);
            
            _currentOffsetX = x;
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
            _transform.position = _path.GetPointAtDistance(_travelledDistance, EndOfPathInstruction.Stop);
            _transform.rotation = _path.GetRotationAtDistance(_travelledDistance, EndOfPathInstruction.Stop);
            _transform.eulerAngles += new Vector3(0, 0, _rotationOffset);
            
            var worldOffset = _transform.TransformDirection(new Vector3(_currentOffsetX, 0, 0));
            _transform.position += worldOffset;
        }
    }
}