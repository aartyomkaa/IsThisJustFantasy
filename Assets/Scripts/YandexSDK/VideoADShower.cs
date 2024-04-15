using Agava.YandexGames;

namespace Assets.Scripts.YandexSDK
{
    internal class VideoADShower : AdShower
    {
        public override void Show()
        {
            VideoAd.Show(OnOpenCallBack, onCloseCallback: OnCloseCallBack);
        }
    }
}
