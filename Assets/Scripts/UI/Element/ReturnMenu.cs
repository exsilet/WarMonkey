using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using UnityEngine;

namespace UI.Element
{
    public class ReturnMenu : MonoBehaviour
    {
        [SerializeField] private string _transferTo;
            
        private IGameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }        

        public void ClickMenu()
        {
            _saveLoadService.SaveProgress();
            _stateMachine.Enter<LoadMenuState, string>(_transferTo);
        }
    }
}