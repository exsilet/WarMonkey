using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class RewardCalculation : MonoBehaviour
    {
        [SerializeField] private ActorUI _actorUI;
        [SerializeField] private Image _rewardWindow;
        [SerializeField] private Image _endGameImage;
        [SerializeField] private TMP_Text _enemiesKilled;
        
        public void GetReward()
        {
            if (_actorUI.IsGameComplete())
            {
                _rewardWindow.gameObject.SetActive(true);
                //_enemiesKilled.text = _actorUI.
            }
            else
                _endGameImage.gameObject.SetActive(true);
        }
    }
}