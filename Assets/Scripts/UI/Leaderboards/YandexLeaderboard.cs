using Agava.YandexGames;
using UnityEngine;

namespace UI.Leaderboards
{
    public class YandexLeaderboard : MonoBehaviour
    {
        [SerializeField] private LeaderboardElement[] _leaderboards;
        [SerializeField] private Transform _leaderBoard;
        [SerializeField] private Transform _listLeaderBoard;
        [SerializeField] private Transform _notLogin;

        private const string WavesLeader = "BestInTheGame";
        private const string Anonymous = "Anonymous";
        private const string TableScore = "TableScore";

        private int _score;
        
        private void Start()
        {
            _score = PlayerPrefs.GetInt(TableScore);
            Debug.Log(" score " + _score);
            SetScore(_score);
        }

        public void Show()
        {
            _leaderBoard.gameObject.SetActive(true);

#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            _listLeaderBoard.gameObject.SetActive(true);
            _notLogin.gameObject.SetActive(false);

            PlayerAccount.RequestPersonalProfileDataPermission();

            Leaderboard.GetEntries(WavesLeader, (result) =>
            {
                for (int i = 0; i < result.entries.Length; i++)
                {
                    var entry = result.entries[i];

                    string name = entry.player.publicName;
                    if (string.IsNullOrEmpty(name))
                        name = Anonymous;

                    _leaderboards[i].gameObject.SetActive(true);
                    _leaderboards[i].Render(_leaderboards[i].ToLeaderboardElementData(entry.rank, name, entry.score));
                }
            });
        }
        else
        {
            _notLogin.gameObject.SetActive(true);
            _listLeaderBoard.gameObject.SetActive(false);
        }
#endif
        }

        public void Authorized()
        {
#if !UNITY_EDITOR
        PlayerAccount.Authorize();
#endif
        }

        public void OnClosetButtonClick()
        {
            _leaderBoard.gameObject.SetActive(false);

            foreach (var element in _leaderboards)
            {
                element.gameObject.SetActive(false);
            }
        }

        private void SetScore(int scoreAmount)
        {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(WavesLeader, (result) =>
        {
            Debug.Log(" score do proverku" + scoreAmount);

            if (result.score < scoreAmount)
            {
                Leaderboard.SetScore(WavesLeader, scoreAmount);
                Debug.Log(" score posle" + scoreAmount);
            }
        });
#endif
        }
    }
}