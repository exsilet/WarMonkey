using UnityEngine;
using UnityEngine.UI;

namespace UI.Leaderboards
{
    public class YandexLeaderboard : MonoBehaviour
    {
        [SerializeField] private LeaderboardElement[] _leaderboards;
        [SerializeField] private Transform _leaderBoard;
        [SerializeField] private Transform _listLeaderBoard;
        [SerializeField] private Transform _notLogin;
        [SerializeField] private Image _iconLeaderboard;
        [SerializeField] private Image _iconListLeaderboard;
        [SerializeField] private Sprite _iconRus;
        [SerializeField] private Sprite _iconEng;
        [SerializeField] private Sprite _iconTur;

        private const string WavesLeader = "TopMonkey";
        private const string Anonymous = "Anonymous";
        private const string TableScore = "TableScore";
        private const string Russian = "Russian";
        private const string English = "English";
        private const string Turkish = "Turkish";

        private string _languageCode;
        private int _score;
        
        private void Start()
        {
            _score = PlayerPrefs.GetInt(TableScore);
            SetScore(_score);
        }

        public void Show()
        {
            ChangeLanguage();
            _leaderBoard.gameObject.SetActive(true);

#if !UNITY_EDITOR
        // if (PlayerAccount.IsAuthorized)
        // {
        //     _listLeaderBoard.gameObject.SetActive(true);
        //     _notLogin.gameObject.SetActive(false);
        //
        //     PlayerAccount.RequestPersonalProfileDataPermission();
        //
        //     Leaderboard.GetEntries(WavesLeader, (result) =>
        //     {
        //         for (int i = 0; i < result.entries.Length; i++)
        //         {
        //             var entry = result.entries[i];
        //
        //             string name = entry.player.publicName;
        //             if (string.IsNullOrEmpty(name))
        //                 name = Anonymous;
        //
        //             _leaderboards[i].gameObject.SetActive(true);
        //             _leaderboards[i].Render(_leaderboards[i].ToLeaderboardElementData(entry.rank, name, entry.score));
        //         }
        //     });
        // }
        // else
        // {
        //     _notLogin.gameObject.SetActive(true);
        //     _listLeaderBoard.gameObject.SetActive(false);
        // }
#endif
        }

        public void Authorized()
        {
#if !UNITY_EDITOR
        //PlayerAccount.Authorize();
#endif
        }

        public void OnClosetButtonClick()
        {
            _leaderBoard.gameObject.SetActive(false);

            foreach (var element in _leaderboards)
            {
                element.gameObject.SetActive(false);
            }

            _languageCode = null;
        }

        private void SetScore(int scoreAmount)
        {
#if !UNITY_EDITOR
        // if (PlayerAccount.IsAuthorized == false)
        //     return;
        //
        // Leaderboard.GetPlayerEntry(WavesLeader, (result) =>
        // {
        //     Debug.Log(" score do proverku" + scoreAmount);
        //
        //     if (result.score < scoreAmount)
        //     {
        //         Leaderboard.SetScore(WavesLeader, scoreAmount);
        //         Debug.Log(" score posle" + scoreAmount);
        //     }
        // });
#endif
        }

        private void ChangeLanguage()
        {
            // _languageCode = LeanLocalization.GetFirstCurrentLanguage();
            //
            // switch (_languageCode)
            // {
            //     case English:
            //         IconLeaderboard(_iconEng);
            //         break;
            //     case Russian:
            //         IconLeaderboard(_iconRus);
            //         break;
            //     case Turkish:
            //         IconLeaderboard(_iconTur);
            //         break;
            //     
            // }
        }

        private void IconLeaderboard(Sprite iconLanguage)
        {
            _iconLeaderboard.sprite = iconLanguage;
            _iconListLeaderboard.sprite = iconLanguage;
        }
    }
}