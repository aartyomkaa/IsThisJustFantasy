using System;

namespace Assets.Scripts.GameLogic
{
    internal interface IHealthDisplayable
    {
        public event Action<float> HealthValueChanged;
    }
}