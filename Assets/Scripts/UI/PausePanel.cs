namespace Assets.Scripts.UI
{
    internal class PausePanel : PanelToggler
    {
        private  Pauser _currentPauser;
        public void SignToPauserEvents(Pauser pauser)
        {
            _currentPauser = pauser;
            _openButton.onClick.AddListener(_currentPauser.Pause);
            _closeButton.onClick.AddListener(_currentPauser.Resume);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(_currentPauser.Pause);
            _closeButton.onClick.RemoveListener(_currentPauser.Resume);
        }
    }
}