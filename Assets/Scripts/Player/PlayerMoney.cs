using Data;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMoney : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private int _currentScore;
        [SerializeField] private int _startSoulIncreaseAmount;
        [SerializeField] private int _rewardRete;

        private int _earnedScore;
        public event UnityAction<int> CurrentSoulChanged;

        public int currentScore => _currentScore;
        public int EarnedScore => _earnedScore;        

        private void Start()
            => CurrentSoulChanged?.Invoke(_currentScore);

        public void AddMoney(int reward)
        {
            _currentScore += reward;
            CurrentSoulChanged?.Invoke(_currentScore);
        }

        public void LoadProgress(PlayerProgress progress)
            => _earnedScore = progress.CurrentSoul;

        public void UpdateProgress(PlayerProgress progress)
        {
            _earnedScore += _currentScore;
            progress.CurrentSoul = _earnedScore;
        }
    }
}