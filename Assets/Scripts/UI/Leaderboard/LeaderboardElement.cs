using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Leaderboard
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private Image _currentColor;
        [SerializeField] private TMP_Text _placeText;
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image _trophy;

        public void Render(LeaderboardElemetData elementData)
        {
            _placeText.SetText(elementData.Place.ToString());
            _nickName.SetText(elementData.Name);
            _scoreText.SetText(elementData.Result.ToString());
        }
    }
}