using PathCreation;
using Player.Controls;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationOffset;
        [SerializeField] private float _limitValue;
        [SerializeField] private InputReader _inputReader;

        private float _cashedHorizontalInput;
        private float _travelledDistance;
        private bool _canMove;

        private void Start()
        {
            _inputReader.MouseEventPerfomed += OnMousePerfomed;
            _inputReader.MouseCancelledEvent += OnMouseCancelled;
        }

        private void OnMouseCancelled()
        {
            _canMove = false;
        }

        private void OnMousePerfomed()
        {
            _canMove = true;
        }

        private void Update()
        {
            FollowPath();

            if (_canMove)
            {
                UpdateHorizontalInput();
                Move();
            }
        }

        private void UpdateHorizontalInput()
        {
            var horizontalInput = _inputReader.MousePosition.x;

            if (horizontalInput != 0)
                _cashedHorizontalInput = horizontalInput;
            
            print("upd");
        }

        private void FollowPath()
        {
            _travelledDistance += _speed * Time.deltaTime;
            transform.position = _pathCreator.path.GetPointAtDistance(_travelledDistance, EndOfPathInstruction.Stop);
            transform.rotation = _pathCreator.path.GetRotationAtDistance(_travelledDistance, EndOfPathInstruction.Stop);
            transform.eulerAngles += new Vector3(0, 0, _rotationOffset);
        }

        private void Move()
        {
            var halfScreen = Screen.width / 2;
            var x = Mathf.Clamp((_cashedHorizontalInput - halfScreen) / halfScreen * _limitValue,-_limitValue,_limitValue);
            var worldOffset = transform.TransformDirection(new Vector3(x, 0, 0));
            
            transform.position += worldOffset;
        }

        private void OnDisable()
        {
            _inputReader.MouseEventPerfomed -= OnMousePerfomed;
            _inputReader.MouseCancelledEvent -= OnMouseCancelled;
        }
    }
}
