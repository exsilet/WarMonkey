using System;
using Logic;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] public Animator _animator;

        private static readonly int MoveHash = Animator.StringToHash("Walking");
        private static readonly int AttackHash = Animator.StringToHash("AttackNormal");
        private static readonly int ReleasingTheButtonHash = Animator.StringToHash("ReleasingTheButton");
        private static readonly int HitHash = Animator.StringToHash("Hit");
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int BlockingHash = Animator.StringToHash("Blocking");
        private static readonly int ReviveHash = Animator.StringToHash("Revive");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
        private readonly int _releasingTheButtonStateHash = Animator.StringToHash("Releasing The Button");
        private readonly int _walkingStateHash = Animator.StringToHash("Run");
        private readonly int _blockingStateHash = Animator.StringToHash("Blocking");
        private readonly int _reviveStateHash = Animator.StringToHash("Revive");
        private readonly int _deathStateHash = Animator.StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;
        public void PlayHit() => _animator.SetTrigger(HitHash);
        public void PlayAttackButtonUp() => _animator.SetTrigger(ReleasingTheButtonHash);
        public void PlayDeath() => _animator.SetTrigger(DieHash);
        public void PlayAttack() => _animator.SetTrigger(AttackHash);
        public void PlayRevive() => _animator.SetTrigger(ReviveHash);
        public void PlayBlocking() => _animator.SetTrigger(BlockingHash);

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
            else if (stateHash == _blockingStateHash)
            {
                state = AnimatorState.Block;
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