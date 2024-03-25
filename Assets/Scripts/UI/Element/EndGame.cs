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
        [SerializeField] private int _delayPanel = 2;

        private List<GameObject> _players = new();
        private HeroDeath _currentPlayer;
        private int _playerKilled;
        private int _maxPlayers;
        private bool _isDeadHero;
        private bool _oneStart;

        public void Construct(GameObject hero, int maxPlayers)
        {
            _players.Add(hero);
            _maxPlayers = maxPlayers;
        }

        private void Start()
        {
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

        public void LoadProgress(PlayerProgress progress)
        {
            _oneStart = progress.WorldData.OneStart;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.OneStart = _oneStart;
        }

        private void Slained(int playerKilled)
        {
            _playerKilled += playerKilled;

            if (_oneStart)
            {
                if (_playerKilled >= _maxPlayers)
                {
                    Invoke(nameof(OpenPanelEndGame), _delayPanel);
                }
            }
            else
            {
                Invoke(nameof(OpenPanelEndGame), _delayPanel);
            }
        }

        private void OpenPanelEndGame()
        {
            _actor.GetRewardEnemy();
            _reward.EndGameReward();
            _rewardEndWindow.gameObject.SetActive(true);
            Time.timeScale = 0;
            _oneStart = false;
        }
    }
}
