using System;
using System.Collections.Generic;
using Infrastructure.StaticData.Players;
using Player;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Element
{
    public class YandexAdServices : MonoBehaviour
    {
        [SerializeField] private Button _resurrectionButton;
        [SerializeField] private RewardCalculation _rewardCalculation;
        [SerializeField] private Image _endGame;
        [SerializeField] private HeroStaticData _heroData;
        
        private List<GameObject> _players = new();
        private int _maxPlayers;
        private int _countScore;
        private int _tableScore;
        private int _factor = 2;
        private int _rewardScore;
        
        public static Action RewardClosed;
        
        public void Construct(GameObject hero, int maxPlayers)
        {
            _players.Add(hero);
            _maxPlayers = maxPlayers;
        }

        private void OnEnable()
        {
            RewardCalculation.ScoreClosed += UpdateScore;
            YandexGame.RewardVideoEvent += Rewarded;
            YandexGame.OpenVideoEvent += OpenVideoReward;
            YandexGame.CloseVideoEvent += CloseVideoReward;
        }

        private void OnDisable()
        {
            RewardCalculation.ScoreClosed += UpdateScore;
            YandexGame.RewardVideoEvent -= Rewarded;
            YandexGame.OpenVideoEvent -= OpenVideoReward;
            YandexGame.CloseVideoEvent -= CloseVideoReward;
        }

        private void UpdateScore(int score)
        {
            _countScore = score;
        }

        private void Rewarded(int id)
        {
            switch (id)
            {
                case 1:
                    AddMoney();
                    break;
                case 2:
                    Resurrection();
                    break;
            }
        }

        private void OpenVideoReward()
        {
            Time.timeScale = 0;
        }

        private void CloseVideoReward()
        {
            RewardClosed?.Invoke();
            Time.timeScale = 1;
        }

        private void AddMoney()
        {
            _rewardScore = _countScore * _factor;
            _rewardCalculation.UpdateScore(_rewardScore);
        }

        private void Resurrection()
        {
            foreach (GameObject player in _players)
            {
                player.GetComponent<HeroHealth>().Current = _heroData.Hp;
                player.GetComponent<HeroAnimator>().ResurrectionMonkey();
                player.GetComponent<BoxCollider>().enabled = true;
                _endGame.gameObject.SetActive(false);
            }
            
            _resurrectionButton.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}