using System.Collections.Generic;
using Data;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Element
{
    public class NextLevel : MonoBehaviour, ISavedProgressReader
    {
        private const string GameScene = "GameScene";
        private const string Construction = "Construction";
        private const string City = "City";
        private const string Police = "Police";
        private const string TransitionScene = "TransitionScene";
        
        [SerializeField] private Button _nextLevel;
        [SerializeField] private HeroStaticData _heroStaticData;
        
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;
        private HeroStaticData _staticData;
        private string TransferTo;
        private int _gameLevel = 1;
        private int _sceneIndex;
        private List<string> _sceneList = new();

        private void Awake()
        {
            _staticData = _heroStaticData;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void Start()
        {
            _sceneList.Add(GameScene);
            _sceneList.Add(Construction);
            _sceneList.Add(City);
            _sceneList.Add(Police);
        }

        private void OnEnable()
            => _nextLevel.onClick.AddListener(Next);

        private void OnDisable()
            => _nextLevel.onClick.RemoveListener(Next);

        public void LoadProgress(PlayerProgress progress)
        {
            _gameLevel = progress.WorldData.CurrentLevels;
        }

        private void Next()
        {
            //RandomNameScene();
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<TransitionState, string>(TransitionScene, _staticData);
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