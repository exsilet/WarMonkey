using System;
using Agava.YandexGames;
using UnityEngine;

namespace UI.Element
{
    public class RewardAD : MonoBehaviour
    {
        [SerializeField] private float _invokeDelay;

        private void Start()
        {
            Invoke(nameof(Reward), _invokeDelay);
        }

        private void Reward()
        {
#if !UNITY_EDITOR
            InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
#endif
        }
        
        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

        private void OnErrorCallback(string description)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            Debug.Log(description);
        }

        private void OnCloseCallback(bool description)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
}