using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Interfaces
{
    internal interface IValuesSender
    {
        public float FirstValue { get; set; }
        public int SecondValue { get; set; }


        public Action<float> FirstValueChanged { get; set; }
        public Action<int> SecondValueChanged { get; set; }

    }
}