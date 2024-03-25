using Data;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.State;
using Infrastructure.StaticData.Players;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class ReturnGame : MonoBehaviour
    {
        private const string KeyScene = "KeyScene";
        
        [SerializeField] private Button _starNewGame;
        [SerializeField] private Button _continueGame;
        [SerializeField] private HeroStaticData _heroStaticData;

        private IPersistentProgressService _progressService;
        private IGameStateMachine _stateMachine;
        
        private bool _oneStart;
        private int _level;
        private string _transferTo;

        private void Start()
        {
            _transferTo = PlayerPrefs.GetString(KeyScene);
            _progressService = AllServices.Container.Single<IPersistentProgressService>();
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
            
            CheckGame();
        }

        public void ReturnLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(_transferTo, _heroStaticData);
        }

        private void CheckGame()
        {
            _oneStart = _progressService.Progress.WorldData.OneStart;
            _level = _progressService.Progress.WorldData.CurrentLevels;

            if (_oneStart && _level > 1)
            {
                _starNewGame.gameObject.SetActive(false);
                _continueGame.gameObject.SetActive(true);
            }
            else
            {
                _starNewGame.gameObject.SetActive(true);
                _continueGame.gameObject.SetActive(false);
            }
        }
    }
}