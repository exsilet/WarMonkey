using System.Collections.Generic;
using Data;
using Infrastructure.Service.SaveLoad;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Element
{
    public class EndGame : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private RewardCalculation _reward;
        [SerializeField] private Image _rewardEndWindow;
        [SerializeField] private ActorUI _actor;

        private List<GameObject> _players = new();
        private HeroDeath _currentPlayer;
        private int _playerKilled;
        private int _maxPlayers;
        private bool _isDeadHero;
        private bool _oneStart;
        private string _currentLevel;

        public void Construct(GameObject hero, int maxPlayers)
        {
            _players.Add(hero);
            _maxPlayers = maxPlayers;
        }

        private void Start()
        {
            _currentLevel = SceneManager.GetActiveScene().name;
            
            foreach (GameObject player in _players)
            {
                _currentPlayer = player.GetComponent<HeroDeath>();
                _currentPlayer.SlainPlayer += Slained;
            }
        }

        public void OnDestroy()
        {
            foreach (GameObject player in _players)
            {
                if (player == null) continue;
                
                _currentPlayer = player.GetComponent<HeroDeath>();
                _currentPlayer.SlainPlayer -= Slained;
            }
        }

        private void Slained(int playerKilled)
        {
            _playerKilled += playerKilled;

            if (_oneStart)
            {
                if (_playerKilled >= _maxPlayers)
                {
                    _actor.GetRewardEnemy();
                    _reward.EndGameReward();
                    _rewardEndWindow.gameObject.SetActive(true);
                    Time.timeScale = 0;
                    _oneStart = false;
                }
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _oneStart = progress.WorldData.OneStart;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.OneStart = _oneStart;
            progress.WorldData.CurrentNameLevel = _currentLevel;
        }
    }
}
