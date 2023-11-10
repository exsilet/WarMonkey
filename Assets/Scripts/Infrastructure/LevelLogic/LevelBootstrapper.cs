using Infrastructure.Service;
using Infrastructure.State;
using UI.Element;
using UnityEngine;

namespace Infrastructure.LevelLogic
{
    public class LevelBootstrapper : MonoBehaviour
    {
        [SerializeField] private LevelScreen _levelScreen;

        public LoadingCurtain Curtain;
        
        private const string NewLevel = "GameScene";
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
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
            _stateMachine.Enter<LoadLevelState, string>(NewLevel);
        }
    }
}