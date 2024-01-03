using System;
using System.Collections;
using System.Globalization;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class StartBattle : MonoBehaviour
    {
        [SerializeField] private GameObject _uiTimer;
        [SerializeField] private float _timerStart;
        [SerializeField] private TMP_Text _textTimer;

        public bool CurrentStartBattle => _startGame;
        private bool _startGame;
        private int _sceneIndex;
        private float _timer;

        private void Start()
        {
            _timer = _timerStart;
            _textTimer.text = _textTimer.ToString();
            _uiTimer.SetActive(true);
            _startGame = false;
            
            StartCoroutine(StartTime());
        }

        private IEnumerator StartTime()
        {
            while (_timerStart >= 0)
            {
                _textTimer.text = $"{_timerStart / 60}";
                _textTimer.text = _timerStart.ToString(CultureInfo.CurrentCulture);
                _timerStart--;
                yield return new WaitForSeconds(1.1f);
            }

            OnEndTimer();
        }
        
        private void OnEndTimer()
        {
            StopCoroutine(StartTime());
            _startGame = true;
            _uiTimer.SetActive(false);
        }    
    }
}