using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class TransitionState : IPayloadedState1<string, HeroStaticData>, IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private string TransferTo;
        private HeroStaticData _heroStaticData;

        private const string GameScene = "GameScene";
        private const string Construction = "Construction";
        private const string City = "City";
        private const string Police = "Police";
        private const string GazStation = "GazMap";
        private List<string> _sceneList = new();

        public TransitionState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _progressService = progressService;
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
            FillList();
            RandomNameScene();
            _gameStateMachine.Enter<LoadLevelState, string>(TransferTo, _heroStaticData);
        }

        private void FillList()
        {
            _sceneList.Add(GameScene);
            _sceneList.Add(Construction);
            _sceneList.Add(City);
            _sceneList.Add(Police);
            _sceneList.Add(GazStation);
        }

        private void RandomNameScene()
        {
            var randomScene = Random.Range(0, _sceneList.Count);

            for (int i = 0; i < _sceneList.Count; i++)
            {
                if (i == randomScene)
                {
                    TransferTo = _sceneList[i];
                }
            }
        }
    }
}