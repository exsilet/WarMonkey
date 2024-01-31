using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Element
{
    public class LevelScreen : MonoBehaviour
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _exitGame;

        public event UnityAction StartLoaded;

        private void OnEnable()
        {
            _startGame.onClick.AddListener(LoadNewGame);
            _exitGame.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(LoadNewGame);
            _exitGame.onClick.RemoveListener(ExitGame);
        }

        private void LoadNewGame()
            => StartLoaded?.Invoke();

        private void ExitGame() => 
            Application.Quit();
    }
}