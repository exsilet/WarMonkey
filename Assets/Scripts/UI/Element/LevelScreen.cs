using Agava.YandexGames;
using Data;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Element
{
    public class LevelScreen : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Button _startGame;

        public event UnityAction StartLoaded;

        private ISaveLoadService _saveLoadService;
        private PlayerProgress _playerProgress;
        private int _monsterQuantity = 3;
        private bool _oneStart;

        private const string GameScene = "GameScene";

        private void Awake() =>
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void OnEnable()
        {
            _startGame.onClick.AddListener(LoadNewGame);

            YandexGamesSdk.GameReady();
            InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback);
        }

        private void OnDisable() =>
            _startGame.onClick.RemoveListener(LoadNewGame);

        public void LoadProgress(PlayerProgress progress)
        {
            _playerProgress = progress;
            _oneStart = progress.WorldData.OneStart;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            Debug.Log("update");
            
            if (!_oneStart)
            {
                Debug.Log("new game");
                progress.WorldData.Level = GameScene;
                progress.WorldData.Score = 0;
                progress.WorldData.MonsterQuantity = _monsterQuantity;
                progress.WorldData.CurrentLevels = 1;
                progress.WorldData.OneStart = true;
            }
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

        private void LoadNewGame()
        {
            _saveLoadService.SaveProgress();
            StartLoaded?.Invoke();
        }
    }
}