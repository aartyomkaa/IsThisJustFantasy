using Assets.Scripts.PlayerComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEngine.Rendering.DebugUI;

public class ColliderEventer : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;

    






    //public static Action<int, PlayerWallet, int, bool> PlayerWentIn;   //����� ���������� ������ ������ ����� ��� ���� int
    //public static Action<int, PlayerWallet, int, bool> PlayerWentOut;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out Player player)) // ������ ��� �������� ����� ���
    //    {
    //        _currentPanel.SetActive(true);
            

    //         _isPlayerIn = true;
    //        PlayerWentIn?.Invoke(_buttonIndex, player.Wallet, _costToBuy, _isPlayerIn);
    //        Debug.Log("������ ����� ��� ������� - " + player.Wallet.Coins);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out Player player))
    //    {
    //        _isPlayerIn = false;
    //        PlayerWentIn?.Invoke(_buttonIndex, player.Wallet, _costToBuy, _isPlayerIn);
    //    }
    //}
}
