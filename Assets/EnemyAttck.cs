using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AnimatorScripts
{
    internal class EnemyAttck : StateMachineBehaviour
    {
        [SerializeField] private AudioSource _source;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("1");
            _source.Play();
        }
    }
}

