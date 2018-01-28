using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OP_Helper : MonoBehaviour
{
    private void Awake()
    {
        Operator.Awake();
    }
    private void Start()
    {
        Operator.Start();
    }
    private void Update()
    {
        Operator.Update();
    }
}
