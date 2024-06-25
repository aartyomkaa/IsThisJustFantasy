using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agava.YandexGames;

namespace Assets.Scripts.UI
{
    internal class LeaderboardEntryView : MonoBehaviour
    {
        private const string AnonymousEn = "Anonymous";
        private const string AnonymousRu = "Аноним";
        private const string AnonymousTr = "İsimsiz";

        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private RawImage _avatar;

        private bool _avatarDownloaded;

        public void SetData(LeaderboardEntryResponse entry)
        {
            if (entry == null)
                throw new ArgumentNullException((nameof(entry)));


            DownloadAvatar(entry.player.profilePicture);
            _playerName.text = SetName(entry.player.publicName);
            _rank.text = entry.rank.ToString();
            _score.text = entry.score.ToString();
        }

        private void DownloadAvatar(string avatarUrl)
        {
            if (_avatarDownloaded == false)
            {
                RemoteImage remoteImage = new(avatarUrl);
                remoteImage.Download(SetAvatar);
            }
        }

        private void SetAvatar(Texture2D texture)
        {
            _avatar.enabled = true;
            _avatar.texture = texture;
            _avatarDownloaded = true;
        }

        private string SetName(string publicName)
        {
            string anon = AnonymousEn;

            if (YandexGamesSdk.Environment.i18n.lang == "ru")
            {
                anon = AnonymousRu;
            }
            else if (YandexGamesSdk.Environment.i18n.lang == "tr")
            {
                anon = AnonymousTr;
            }

            if (string.IsNullOrEmpty(publicName))
                publicName = anon;

            return publicName;
        }
    }
}
