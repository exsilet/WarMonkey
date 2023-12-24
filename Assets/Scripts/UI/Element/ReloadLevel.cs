using Infrastructure;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UnityEngine;

namespace UI.Element
{
    public class ReloadLevel : MonoBehaviour
    {
        [SerializeField] private HeroStaticData _heroStaticData;
        
        private const string NewLevel = "GameScene";

        private IGameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private HeroStaticData _staticData;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _staticData = _heroStaticData;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void Reload()
        {
            _saveLoadService.ResetProgress();
            _stateMachine.Enter<LoadLevelState, string>(NewLevel, _staticData);
        }
    }
}