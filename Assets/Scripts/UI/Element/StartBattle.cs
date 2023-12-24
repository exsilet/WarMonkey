using System;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class StartBattle : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        public bool CurrentStartBattle => _startGame;
        private bool _startGame = false;
        private int _sceneIndex;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            _startGame = true;
        }
    }
}