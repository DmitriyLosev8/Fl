using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Containers;
using Assets.Scripts.Yandex;

namespace Agava.YandexGames.Samples
{
    public class LeaderboardDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _ranks;
        [SerializeField] private TMP_Text[] _leaderNames;
        [SerializeField] private TMP_Text[] _scoreList;
        [SerializeField] private LightContainer _lightContainer;
        [SerializeField] private string _leaderBoardName = "Leaderboard";
        [SerializeField] private InputField _cloudSaveDataInputField;
        [SerializeField] private TMP_Text _anonimus;
        [SerializeField] private YandexAuthorizationPanel _authorizationPanel;

        private void Awake()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                _authorizationPanel.gameObject.SetActive(true);    
            }

            if (PlayerAccount.IsAuthorized == true)
            {
                OpenLeaderboard();
            }
            else
            {
                gameObject.SetActive(false);
            }           
        }

        public void OpenLeaderboard()
        {
            SetLeaderboardScore();

            Leaderboard.GetEntries(_leaderBoardName, result =>
            {
                int leaderNumber;

                if (result.entries.Length >= _leaderBoardName.Length)
                    leaderNumber = _leaderBoardName.Length;
                else
                    leaderNumber = result.entries.Length;

                for (int i = 0; i < leaderNumber; i++)
                {
                    string name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = _anonimus.text;
                    }

                    _leaderNames[i].text = name;
                    _scoreList[i].text = result.entries[i].formattedScore;
                    _ranks[i].text = result.entries[i].rank.ToString();
                }
            },

            error =>
            {
            }
            );
        }

        public void SetLeaderboardScore()
        {
            Leaderboard.GetPlayerEntry(_leaderBoardName, OnSuccessCallback);
        }

        private void OnSuccessCallback(LeaderboardEntryResponse result)
        {
            if (result == null || _lightContainer.Lights > result.score)
            {
                Leaderboard.SetScore(_leaderBoardName, _lightContainer.Lights);
            }
        }
    }
}