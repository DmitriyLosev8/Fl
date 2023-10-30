using System.Collections;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

        private int _countOfLights;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;

            if (PlayerAccount.IsAuthorized == false)
            {
                PlayerAccount.Authorize();
            }

            if (PlayerAccount.IsAuthorized == true)
            {
                OpenLeaderboard();
            }
            else
            {
                gameObject.SetActive(false);
            }
            
            _countOfLights = _lightContainer.Lights;
        }


        private void OnSuccessCallback(LeaderboardEntryResponse result)
        {
            if (result == null || _lightContainer.Lights > result.score)
            {
                Leaderboard.SetScore(_leaderBoardName, _lightContainer.Lights);
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
            });
        }

        public void SetLeaderboardScore()
        {
            Leaderboard.GetPlayerEntry(_leaderBoardName, OnSuccessCallback);
        }
    }
}


