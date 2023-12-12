using Data;
using Infrastructure.Service.SaveLoad;
using Logic;
using UnityEngine;

namespace UI.Element
{
    public class ActorUI : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private SpawnerController _spawnerController;
        [SerializeField] private AudioSource _endGameMusic;
        [SerializeField] private RewardCalculation _reward;

        private int _gameLevel;
        private GameObject _spawner;
        
        private void Awake()
        {
            _spawnerController = _spawner.GetComponent<SpawnerController>();
        }

        private void OnEnable()
        {
            _spawnerController.LevelCompleted += CompleteLevel;
            _spawnerController.LevelCompleted += _reward.GetReward;
        }

        private void OnDestroy()
        {
            _spawnerController.LevelCompleted -= CompleteLevel;
            _spawnerController.LevelCompleted -= _reward.GetReward;
        }

        public void LoadProgress(PlayerProgress progress) => 
            progress.HeroState.GameLevel = _gameLevel;

        public void UpdateProgress(PlayerProgress progress) => 
            _gameLevel = progress.HeroState.GameLevel;

        public bool IsGameComplete()
        {
            if (_spawnerController.IsLevelComplete == true && _gameLevel == 3)
            {
                _endGameMusic.Play();
                return true;
            }                
            return false;
        }
        
        private void CompleteLevel()
        {
            _gameLevel++;
        }
    }
}