using Data;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class SpawnerController : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private SpawnPoint[] _spawners;
        [SerializeField] private int _initialNumberSpawns = 3;
        [SerializeField] private int _transitionInterval = 2;
        [SerializeField] private int _currentLevel;
        [SerializeField] private GameObject _treasureBackground;

        private int _wavesCount;
        private SpawnPoint _currentSpawner;
        private bool _isLevelComplete;
        private bool _canChange = false;
        private float _numbersKilledEnemies;

        public bool IsLevelComplete => _isLevelComplete;

        public event UnityAction WaveCompleted;
        public event UnityAction LevelCompleted;

        private void OnEnable()
        {
            WaveCompleted += ShowBackground;
        }

        private void OnDestroy()
        {
            WaveCompleted -= ShowBackground;
        }  
        
        private void SetSpawner(int index)
        {
            _currentSpawner = _spawners[index];
            //_spawned = _currentSpawner.Spawned;
        }

        public void NextWave()
        {
            if (_transitionInterval != _spawners.Length)
            {
                _spawners[_transitionInterval].gameObject.SetActive(true);

                if (_canChange == true)
                {
                    SetSpawner(++_transitionInterval);
                    _spawners[_transitionInterval].gameObject.SetActive(true);
                }
                else
                    SetSpawner(_transitionInterval);

                _canChange = true;
            }
        }

        public void AddNewSpawner()
        {
            _initialNumberSpawns += _transitionInterval;
            _currentLevel++;
        }

        public void ActiveSpawn()
        {
            for (int i = 0; i < _initialNumberSpawns; i++)
            {
                _spawners[i].gameObject.SetActive(true);
            }
        }

        public void OnEnemyDied()
        {
            _numbersKilledEnemies++;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _initialNumberSpawns = progress.HeroState.CurrentNumberSpawners;
            _currentLevel = progress.HeroState.GameLevel;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentNumberSpawners = _initialNumberSpawns;
            progress.HeroState.GameLevel = _currentLevel;
        }
        
        private void CheckLastWave()
        {
            if (_wavesCount == 0)
            {
                _isLevelComplete = true;
                LevelCompleted?.Invoke();
            }
        }

        private void ShowBackground()
            => _treasureBackground.SetActive(true);
    }
}