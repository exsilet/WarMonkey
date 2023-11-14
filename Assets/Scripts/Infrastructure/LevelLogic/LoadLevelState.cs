using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Logic;
using Player;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private IState _stateImplementation;
        private Camera _camera;
        private List<HeroMover> _heroMovers = new();

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitSpawners();
            GameObject selectedUnits = _gameFactory.CreateSelectUnits();

            foreach (GameObject player in GameObject.FindGameObjectsWithTag(InitialPointTag))
            {
                GameObject hero = _gameFactory.CreateHero(player);
                selectedUnits.GetComponent<SelectUnit>().Construct(hero.GetComponent<Selectable>());
            }

            GameObject hud = _gameFactory.CreateHud();
            InitHud();
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag("EnemySpawner"))
            {
                var spawner = spawnerObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InitHud()
        {
            //hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<Hero>(),hero.GetComponent<HeroHealth>(), hero.GetComponent<CastSpell>());
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);

        }
    }
}
