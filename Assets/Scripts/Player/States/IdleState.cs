using UnityEngine;
using Utilities.FSM;

namespace Player.States
{
    public class IdleState : FsmState
    {
        private readonly Animator _animator;
        private readonly int _hash;
        
        public IdleState(FiniteStateMachine finiteStateMachine, Animator animator, int hash) : base(finiteStateMachine)
        {
            _animator = animator;
            _hash = hash;
        }

        public override void Enter()
        {
            _animator.Play(_hash);
        }
    }
}