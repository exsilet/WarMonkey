using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Element
{
    public class StartBattle : MonoBehaviour
    {
        private const string KeyScene = "KeyScene";
        
        [SerializeField] private GameObject _uiTimer;
        [SerializeField] private float _timerStart;
        [SerializeField] private TMP_Text _textTimer;

        public bool CurrentStartBattle => _startGame;
        private bool _startGame;
        private int _sceneIndex;
        private float _timer;
        private string _currentScene;

        private void Start()
        {
            _timer = _timerStart;
            _textTimer.text = _textTimer.ToString();
            _uiTimer.SetActive(true);
            _startGame = false;

            _currentScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString(KeyScene, _currentScene);
            
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