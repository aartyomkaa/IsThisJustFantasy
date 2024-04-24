using Assets.Scripts.PlayerComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelZone : MonoBehaviour
{
    private bool _hasLeveledUp = false;

    public event Action PlayerWentIn;
    public event Action PlayerWentOut;
    public event Action LevelUped;

    private int _countOfPlayersCameIn = 0;
    private int _firstPlayersCameIn = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayerWentIn?.Invoke();

            if (_hasLeveledUp == false)
            {
                player.LevelUp();
                _hasLeveledUp = true;
            }

            _countOfPlayersCameIn++;
           
            if (_countOfPlayersCameIn == _firstPlayersCameIn)
            {
                LevelUped?.Invoke();
            }  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayerWentOut?.Invoke();
        }
    }
}
