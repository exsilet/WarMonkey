using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.Service.StaticData;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using Logic;
using Player;
using UI.Element;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.LevelLogic
{
    public class LoadLevelState : IPayloadedState1<string, HeroStaticData>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private Camera _camera;
        private HeroStaticData _heroStaticData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticData = staticDataService;
        }

        public void EnterThreeParameters(string sceneName, HeroStaticData heroData)
        {
            _heroStaticData = heroData;
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
            GameObject hud = _gameFactory.CreateHud();
            
            InitSpawners(hud);
            
            GameObject selectedUnits = _gameFactory.CreateSelectUnits();

            foreach (GameObject player in GameObject.FindGameObjectsWithTag(InitialPointTag))
            {
                GameObject hero = _gameFactory.CreateHero(_heroStaticData, player.transform);
                selectedUnits.GetComponent<SelectUnit>().Construct(hero.GetComponent<Selectable>());
            }
            
            InitHud();
        }

        private void InitSpawners(GameObject hudBattle)
        {
            // foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag("EnemySpawner"))
            // {
            //     var spawner = spawnerObject.GetComponent<SpawnPoint>();
            //     _gameFactory.Register(spawner);
            // }
            
            string sceneKey = _progressService.Progress.HeroState.GameLevel.ToString();
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);
      
            foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
                _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.EnemyTypeID);
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
