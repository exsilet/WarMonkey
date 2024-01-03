using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] public Animator _animator;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("AttackNormal");
        private static readonly int ReleasingTheButton = Animator.StringToHash("ReleasingTheButton");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Revive = Animator.StringToHash("Revive");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
        private readonly int _releasingTheButtonStateHash = Animator.StringToHash("Releasing The Button");
        private readonly int _walkingStateHash = Animator.StringToHash("Move");
        private readonly int _reviveStateHash = Animator.StringToHash("Revive");
        private readonly int _deathStateHash = Animator.StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;
        public void PlayHit() => _animator.SetTrigger(Hit);
        public void Move()
        {
            _animator.SetBool(IsMoving, true);
        }

        public void StopMoving() => _animator.SetBool(IsMoving, false);
        public void PlayAttackButtonUp() => _animator.SetTrigger(ReleasingTheButton);
        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayAttack() => _animator.SetTrigger(Attack);
        public void PlayRevive() => _animator.SetTrigger(Revive);

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
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _releasingTheButtonStateHash)
            {
                state = AnimatorState.AttackDawnUp;
            }
            else if (stateHash == _walkingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else if (stateHash == _reviveStateHash)
            {
                state = AnimatorState.Revive;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}