using System.Collections.Generic;
using Data;
using Enemy;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Logic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ActorUI : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Image _rewardWindow;
        [SerializeField] private AudioSource _endGameMusic;
        [SerializeField] private RewardCalculation _reward;
        [SerializeField] private int _increasingNumberMonsters = 2;

        private List<GameObject> _spawnersPoint = new();
        private int _gameLevel = 1;
        private int _enemyKilled;
        private int _maxSpawners;
        private SpawnPoint _currentSpawner;
        private int _monsterQuantity = 3;

        public void Construct(GameObject enemySpawner, int monsterQuantity)
        {
            _spawnersPoint.Add(enemySpawner);
            _maxSpawners = monsterQuantity;
        }

        private void Start()
        {
            Time.timeScale = 1;
            
            foreach (GameObject spawner in _spawnersPoint)
            {
                _currentSpawner = spawner.GetComponent<SpawnPoint>();
                _currentSpawner.Slained += Slained;
            }
        }

        public void GetRewardEnemy()
        {
            foreach (var spawn in _spawnersPoint)
            {
                if(spawn.GetComponent<SpawnPoint>().Slain)
                    _reward.GetReward(spawn.GetComponentInChildren<LootEnemy>().Money);
            }
        }

        private void Slained(int enemyKilled)
        {
            _enemyKilled += enemyKilled;

            if (_enemyKilled == _maxSpawners)
            {
                CompleteLevel();

                foreach (GameObject spawner in _spawnersPoint)
                {
                    _reward.GetReward(spawner.GetComponentInChildren<LootEnemy>().Money);
                }

                _reward.ScoreLevel();
                _reward.SetReward();
                _rewardWindow.gameObject.SetActive(true);

                foreach (GameObject spawner in _spawnersPoint)
                {
                    _currentSpawner = spawner.GetComponent<SpawnPoint>();
                    _currentSpawner.Slained -= Slained;
                }
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _monsterQuantity = progress.WorldData.MonsterQuantity;
            Debug.Log(_monsterQuantity + " kolu4ectvo monsctrod load");
            _gameLevel = progress.WorldData.CurrentLevels;
            Debug.Log(_gameLevel + " kolu4ectvo monsctrod load");
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.MonsterQuantity = _monsterQuantity;
            Debug.Log(_monsterQuantity + " kolu4ectvo monsctrod save");
            progress.WorldData.CurrentLevels = _gameLevel;
            Debug.Log(_gameLevel + " kolu4ectvo monsctrod save");
        }

        private void CompleteLevel()
        {
            _monsterQuantity += _increasingNumberMonsters;
            _gameLevel++;
        }
    }
}