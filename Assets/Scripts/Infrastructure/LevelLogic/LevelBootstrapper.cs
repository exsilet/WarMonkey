using Infrastructure.Service;
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

        private void Awake()
        {
            _staticData = _heroStaticData;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
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
            _stateMachine.Enter<LoadLevelState, string>(NewLevel, _staticData);
        }
    }
}