using Data;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Element
{
    public class NextLevel : MonoBehaviour, ISavedProgressReader
    {
        private const string TransitionScene = "TransitionScene";

        [SerializeField] private Button _nextLevel;
        [SerializeField] private HeroStaticData _heroStaticData;

        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;
        private HeroStaticData _staticData;
        private string TransferTo;
        private int _gameLevel = 1;
        private int _sceneIndex;

        private void Awake()
        {
            _staticData = _heroStaticData;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnEnable()
            => _nextLevel.onClick.AddListener(Next);

        private void OnDisable()
            => _nextLevel.onClick.RemoveListener(Next);

        public void LoadProgress(PlayerProgress progress) => 
            _gameLevel = progress.WorldData.CurrentLevels;

        private void Next()
        {
            YandexGame.FullscreenShow();
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<TransitionState, string>(TransitionScene, _staticData);
        }
    }
}