using Data;
using Infrastructure.Service.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class RewardCalculation : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private ActorUI _actorUI;
        [SerializeField] private Image _rewardWindow;
        [SerializeField] private Image _endGameImage;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _scoreLevel;
        [SerializeField] private TMP_Text _scoreEndGame;

        private float _timeDelay = 1.5f;
        private int _currentReward;
        private int _countScore;
        
        public void GetReward(int reward) => 
            _currentReward += reward;

        public void SetReward()
        {
            _countScore += _currentReward;
            _scoreText.text = _countScore.ToString();
            
            Invoke(nameof(StopTime), _timeDelay);
        }

        public void ScoreLevel()
        {
            _scoreLevel.text = _currentReward.ToString();
        }

        public void EndGameReward()
        {
            _countScore += _currentReward;
            _scoreEndGame.text = _countScore.ToString();
            
            Invoke(nameof(StopTime), _timeDelay);
        }

        private void StopTime() => 
            Time.timeScale = 0;

        public void LoadProgress(PlayerProgress progress)
        {
            _countScore = progress.WorldData.Score;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.Score = _countScore;
        }
    }
}