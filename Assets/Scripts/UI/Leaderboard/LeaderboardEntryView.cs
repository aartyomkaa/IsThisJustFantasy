using Agava.YandexGames;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
        [SerializeField] private Image _image;

        private Coroutine _setImage;

        private void Awake()
        {
            gameObject.SetActive(true);
        }

        public void SetData(LeaderboardEntryResponse entry)
        {
            if (entry == null)
                return;

            Debug.Log("SETTING DATA");

            //if (_setImage != null)
                //StopCoroutine(_setImage);
            //else
                //_setImage = StartCoroutine(SetImage(entry.player.profilePicture));

            Debug.Log($"NAME: {entry.player.publicName}");
            Debug.Log($"RANK: {entry.rank}");
            Debug.Log($"Score: {entry.score}");
            _playerName.text = SetName(entry.player.publicName);
            _rank.text = entry.rank.ToString();
            _score.text = entry.score.ToString();
        }

        private IEnumerator SetImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

            Debug.Log("SETTING IMAGE");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError
                || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                throw new System.Exception();
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                _image.sprite = sprite;
            }
        }

        private string SetName(string publicName)
        {
            Debug.Log("SETTING NAME");

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
