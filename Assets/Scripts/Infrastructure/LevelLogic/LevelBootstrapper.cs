using Data;
using Infrastructure.Service;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UI.Element;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class LevelBootstrapper : MonoBehaviour
    {
        [SerializeField] private LevelScreen _levelScreen;
        [SerializeField] private HeroStaticData _heroStaticData;

        public LoadingCurtain Curtain;

        private const string NewLevel = "GameScene";
        private IGameStateMachine _stateMachine;
        private HeroStaticData _staticData;
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;

        private void Awake()
        {
            _staticData = _heroStaticData;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _progressService = AllServices.Container.Single<IPersistentProgressService>();
        }

        private void OnEnable()
        {
            _levelScreen.StartLoaded += OnNewGameLoaded;
        }


        private void OnDisable()
        {
            _levelScreen.StartLoaded -= OnNewGameLoaded;
        }

        private void OnNewGameLoaded()
        {
            _saveLoadService.ResetProgress();
            _progressService.Progress = NewProgress();
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<LoadLevelState, string>(NewLevel, _staticData);
        }
        
        private PlayerProgress NewProgress()
        {            
            var progress =  new PlayerProgress(initialLevel: NewLevel);
            //progress.HeroState.ResetHP();

            return progress;
        }
    }
}