using Agava.YandexGames;
using Data;
using Infrastructure.Service.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class RewardCalculation : MonoBehaviour, ISavedProgress
    {
        private const string TableScore = "TableScore";
        
        [SerializeField] private ActorUI _actorUI;
        [SerializeField] private Image _rewardWindow;
        [SerializeField] private Image _endGameImage;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _scoreLevel;
        [SerializeField] private TMP_Text _scoreEndGame;
        [SerializeField] private Button _reward;

        private float _timeDelay = 1.5f;
        private int _currentReward;
        private int _countScore;
        private int _tableScore;
        private int _factor = 2;
        private int _rewardScore;

        public void LoadProgress(PlayerProgress progress)
        {
            _countScore = progress.WorldData.Score;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.Score = _countScore;
        }

        public void GetReward(int reward) => 
            _currentReward += reward;

        public void SetReward()
        {
            _countScore += _currentReward;
            _scoreText.text = _countScore.ToString();

            AccountCheck();
            
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

            AccountCheck();
            
            Invoke(nameof(StopTime), _timeDelay);
        }

        public void TakeReward()
        {
            VideoAd.Show(OnOpenCallback,OnCloseCallback, OnRewardedCallback, OnErrorCallback);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            _rewardScore = _countScore * _factor;
            AudioListener.pause = true;
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            UpdateScore();
        }

        private void OnRewardedCallback() => _rewardScore = _countScore * _factor;

        private void OnErrorCallback(string description)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            _rewardScore = _countScore * _factor;
            Debug.Log(description);
        }

        private void UpdateScore()
        {
            _countScore = _rewardScore;
            _scoreText.text = _countScore.ToString();

            AccountCheck();
            
            Invoke(nameof(StopTime), _timeDelay);
        }

        private void StopTime() => 
            Time.timeScale = 0;

        private void AccountCheck()
        {
            _tableScore = PlayerPrefs.GetInt(TableScore);
            
            if (_tableScore < _countScore)
            {
                _tableScore = _countScore;
                PlayerPrefs.SetInt(TableScore, _tableScore);
                PlayerPrefs.Save();
            }
        }
    }
}