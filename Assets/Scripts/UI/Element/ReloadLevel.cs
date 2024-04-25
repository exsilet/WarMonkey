using Data;
using Infrastructure;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Element
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField] private HeroStaticData _heroStaticData;
        [SerializeField] private Button _reloadGame;
        
        private const string TransitionScene = "TransitionScene";
        private const string GameScene = "GameScene";

        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;
        private SceneLoader _sceneLoader;
        private HeroStaticData _staticData;
        private PlayerProgress _playerProgress;
        private int _monsterQuantity = 3;
        private IPersistentProgressService _progressService;

        private void Awake()
        {
            _staticData = _heroStaticData;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _progressService = AllServices.Container.Single<IPersistentProgressService>();
        }

        public void SaveReload()
        {
            _saveLoadService.ResetProgress();
            _saveLoadService.SaveProgress();
        }

        public void Reload()
        {
            YandexGame.FullscreenShow();
            _saveLoadService.ResetProgress();
            _progressService.Progress = NewProgress();
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<TransitionReloadState, string>(TransitionScene, _staticData);
        }

        private PlayerProgress NewProgress()
        {
            var progress =  new PlayerProgress(initialLevel: GameScene)
            {
                WorldData =
                {
                    Level = GameScene,
                    Score = 0,
                    MonsterQuantity = _monsterQuantity,
                    CurrentLevels = 1,
                    OneStart = true
                }
            };

            return progress;
        }
    }
}