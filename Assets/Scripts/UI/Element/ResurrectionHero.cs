using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Infrastructure.StaticData.Players;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ResurrectionHero : MonoBehaviour
    {
        [SerializeField] private HeroStaticData _heroData;
        [SerializeField] private Button _resurrectionButton;
        [SerializeField] private Image _endGame;
        
        private List<GameObject> _players = new();
        private int _maxPlayers;
        private HeroStaticData _currentHero;

        public void Construct(GameObject hero, int maxPlayers)
        {
            _players.Add(hero);
            _maxPlayers = maxPlayers;
        }

        public void TakeReward()
        {
#if !UNITY_EDITOR
            VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
#endif
        }

        public void Resurrection()
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

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

        private void OnRewardedCallback()
        {
            Resurrection();
        }

        private void OnErrorCallback(string description)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            Resurrection();
        }
    }
}