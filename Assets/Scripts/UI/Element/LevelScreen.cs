using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Element
{
    public class LevelScreen : MonoBehaviour
    {
        [SerializeField] private Button _startGame;

        public event UnityAction StartLoaded;

        private void OnEnable()
        {
            _startGame.onClick.AddListener(LoadNewGame);
        }

        private void OnDisable()
        {
            _startGame.onClick.RemoveListener(LoadNewGame);
        }

        private void LoadNewGame()
            => StartLoaded?.Invoke();
    }
}