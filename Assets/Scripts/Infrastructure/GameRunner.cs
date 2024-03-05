using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
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
#if !UNITY_WEBGL || !UNITY_EDITOR
            while (!YandexGamesSdk.IsInitialized)
                yield return YandexGamesSdk.Initialize();

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.GetCloudSaveData(OnSuccessCallback, OnErrorCallback);
            else
                StartGame();
            
#endif
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
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}