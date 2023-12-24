using Data;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ReturnMenu : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public string TransferTo;
        private IGameStateMachine _stateMachine;
        private IPersistentProgressService _progressService;

        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _progressService = AllServices.Container.Single<IPersistentProgressService>();
        }        

        public void ClickMenu()
        {
            _saveLoadService.ResetProgress();
            _progressService.Progress = NewProgress();
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<LoadMenuState, string>(TransferTo);
        }
        
        private PlayerProgress NewProgress()
        {            
            var progress =  new PlayerProgress(initialLevel: TransferTo);
            //progress.HeroState.ResetHP();

            return progress;
        }
    }
}