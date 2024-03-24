using System;
using Agava.YandexGames;
using Logic;
using UnityEngine;

namespace Player
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("AttackNormal");
        private static readonly int IsHold = Animator.StringToHash("IsHold");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Blocking = Animator.StringToHash("Blocking");
        private static readonly int Revive = Animator.StringToHash("Revive");
        
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("RangeAttack1");
        private readonly int _releasingTheButtonStateHash = Animator.StringToHash("RangeAttack1 0");
        private readonly int _blockingStateHash = Animator.StringToHash("Blocking");
        private readonly int _reviveStateHash = Animator.StringToHash("Revive");
        private readonly int _deathStateHash = Animator.StringToHash("Die");
        
        private Animator _animator;
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }

        private void Awake() => _animator = GetComponent<Animator>();

        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
        }

        public void PlayAttack() => _animator.SetTrigger(Attack);
        public bool IsAttacking => State == AnimatorState.Attack;
        public void PlayRevive() => _animator.SetTrigger(Revive);
        public void PlayBlocking() => _animator.SetTrigger(Blocking);
        public void PlayDeath() => _animator.SetTrigger(Die);
        
        public void Hold() => _animator.SetBool(IsHold, false);
        public void StopHolding() => _animator.SetBool(IsHold, true);
        public void ResurrectionMonkey() => _animator.SetBool(Revive, true);

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
            else if (stateHash == _releasingTheButtonStateHash)
            {
                state = AnimatorState.AttackDawnUp;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }
    }
}