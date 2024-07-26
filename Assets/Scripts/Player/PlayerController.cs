using PathCreation;
using Player.Controls;
using Player.States;
using UnityEngine;
using Utilities.FSM;
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
        private FiniteStateMachine _finiteStateMachine = new();
        
        private float _travelledDistance;

        private bool _isStarted = false;
        private bool _canMove;

        [Inject]
        private void Construct(PathCreator pathCreator)
        {
            _path = pathCreator.path;
        }

        private void Start()
        {
            _inputReader.MousePerfomedEvent += OnMousePerfomed;
            
            _finiteStateMachine.Add(new IdleState(_finiteStateMachine, _animator, Animator.StringToHash("idle")));
            _finiteStateMachine.Add(new WalkState(_finiteStateMachine, transform, _path, _animator, _inputReader, _speed, _rotationOffset, _limitValue, Animator.StringToHash("walk")));
            _finiteStateMachine.Add(new WinState(_finiteStateMachine, _animator, Animator.StringToHash("victory")));
            _finiteStateMachine.Add(new LoseState(_finiteStateMachine, _animator, Animator.StringToHash("lose")));

            _finiteStateMachine.Set<IdleState>();
        }
        

        private void OnMousePerfomed()
        {
            if (_isStarted)
                return;
            
            _isStarted = true;
            _finiteStateMachine.Set<WalkState>();
        }

        private void Update()
        {
            _finiteStateMachine.Update();
        }
        
        private void OnDisable()
        {
            _inputReader.MousePerfomedEvent -= OnMousePerfomed;
            _finiteStateMachine.Dispose();
        }

        public void SetWin()
        {
            _finiteStateMachine.Set<WinState>();
        }

        public void SetLose()
        {
            _finiteStateMachine.Set<LoseState>();
        }
    }
}
