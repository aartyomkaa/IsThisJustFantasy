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
    public event Action GameLevelUped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            PlayerWentIn?.Invoke();

            if (_hasLeveledUp == false)
            {
                player.LevelUp();
                GameLevelUped?.Invoke();
                _hasLeveledUp = true;
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
