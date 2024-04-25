using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake() => 
            StartCoroutine(LaunchSDK());

        private IEnumerator LaunchSDK()
        {
// #if //!UNITY_WEBGL || !UNITY_EDITOR
//             while (!YandexGamesSdk.IsInitialized)
//                 yield return YandexGamesSdk.Initialize();
//
//             if (PlayerAccount.IsAuthorized)
//                 PlayerAccount.GetCloudSaveData(OnSuccessCallback, OnErrorCallback);
//             else
//                 StartGame();
//             
// #endif
            yield return new WaitForSeconds(0f);
            StartGame();
        }
        
        private void OnSuccessCallback(string data)
        {
            StartGame();
        }

        private void OnErrorCallback(string _) => 
            StartGame();

        private void StartGame()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            
            if (bootstrapper != null)
                return;
            
            Instantiate(BootstrapperPrefab);
        }
        
        private void OnEnable()
        {
            //Application.focusChanged += OnInBackgroundChangeApp;
            //WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            //Application.focusChanged -= OnInBackgroundChangeApp;
            //WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool inApp)
        {
            MuteAudio(!inApp);
            PauseGame(!inApp);
        }

        private void OnInBackgroundChangeWeb(bool inBackground)
        {
            MuteAudio(inBackground);
            PauseGame(inBackground);
        }

        private void MuteAudio(bool value)
        {
            AudioListener.pause = value;
            AudioListener.volume = value ? 0f : 1f;
        }

        private void PauseGame(bool value)
        {
            Time.timeScale = value ? 0f : 1f;
        }
    }
}