using Assets.Scripts.PlayerComponents;
using System;
using UnityEngine;

public class NextLevelZone : MonoBehaviour
{
    private bool _hasLeveledUp = false;

    public event Action PlayerWentIn;
    public event Action PlayerWentOut;
    public event Action GameLevelUped;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

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

    public void OnAllWavesDefeated()
    {
        gameObject.SetActive(true);
    }
}
