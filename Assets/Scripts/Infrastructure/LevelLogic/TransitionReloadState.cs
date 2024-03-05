using Data;
using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;

namespace Infrastructure.LevelLogic
{
    public class TransitionReloadState : IPayloadedState1<string, HeroStaticData>, IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private HeroStaticData _heroStaticData;
        private readonly ISaveLoadService _saveLoadService;

        private const string GameScene = "GameScene";

        public TransitionReloadState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void EnterThreeParameters(string sceneName, HeroStaticData heroData)
        {
            _heroStaticData = heroData;
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _curtain.Hide();

        public void Enter()
        {
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(GameScene, _heroStaticData);
        }
    }
}