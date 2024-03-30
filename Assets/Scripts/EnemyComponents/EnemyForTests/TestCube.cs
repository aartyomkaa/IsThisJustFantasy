using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    public float Range;
    public LayerMask LayerMask;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * Range, Color.red, 0.1f);

        Debug.Log(Physics.Raycast(transform.position, transform.forward, Range, LayerMask));
    }
}