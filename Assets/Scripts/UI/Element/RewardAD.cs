using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Element
{
    public class RewardAD : MonoBehaviour
    {
        [SerializeField] private Button _rewardButton;
        
        private void OnEnable()
        {
            YandexAdServices.RewardClosed += RewardClosed;
        }

        private void OnDisable()
        {
            YandexAdServices.RewardClosed -= RewardClosed;
        }

        public void WatchVideoAd(int id)
        {
            YandexGame.RewVideoShow(id);
            _rewardButton.interactable = false;
        }

        private void RewardClosed()
        {
            _rewardButton.interactable = true;
        }
        
//         [SerializeField] private float _invokeDelay;
//
//         private void Start()
//         {
//             Invoke(nameof(Reward), _invokeDelay);
//         }
//
//         private void Reward()
//         {
// #if !UNITY_EDITOR
//             YandexGame.FullscreenShow();
// #endif
//         }
    }
}