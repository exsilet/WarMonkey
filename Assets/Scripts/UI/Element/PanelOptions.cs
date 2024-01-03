using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class PanelOptions : MonoBehaviour
    {
        [SerializeField] private GameObject _panelOptions;
        [SerializeField] private Button _buttonOption;
        [SerializeField] private Button _exitOptions;

        private void Start() => 
            _panelOptions.SetActive(false);

        private void OnEnable()
        {
            _buttonOption.onClick.AddListener(OpenPanelOption);
            _exitOptions.onClick.AddListener(ExitPanelOption);
        }

        private void OnDisable()
        {
            _buttonOption.onClick.RemoveListener(OpenPanelOption);
            _exitOptions.onClick.RemoveListener(ExitPanelOption);
        }

        public void ExitPanelOption()
        {
            _panelOptions.SetActive(false);
            Time.timeScale = 1;
        }

        private void OpenPanelOption()
        {
            Time.timeScale = 0;
            _panelOptions.SetActive(true);
        }
    }
}