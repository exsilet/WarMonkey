using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.State;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class LoadMenuState : IPayloadedState<string>, IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadMenuState(GameStateMachine  gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }        

        public void Exit()
        {
            _curtain.Hide();
        }

        public void Enter() { }        

        private void OnLoaded()
        {
            InitGameWorld();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject hud = _gameFactory.CreateHudMenu();
        }
    }
}