using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private RewardCalculation _reward;
        [SerializeField] private Image _rewardEndWindow;
        [SerializeField] private ActorUI _actor;

        private List<GameObject> _players = new();
        private int _playerKilled;
        private int _maxPlayers;
        private HeroDeath _currentPlayer;

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

        private void Slained(int playerKilled)
        {
            _playerKilled += playerKilled;
            
            if (_playerKilled == _maxPlayers)
            {
                _actor.GetRewardEnemy();
                _reward.EndGameReward();
                _rewardEndWindow.gameObject.SetActive(true);
            }
        }
    }
}
