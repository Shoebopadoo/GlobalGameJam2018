using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OP_Helper : MonoBehaviour
{
    [SerializeField]
    LifeTracker _lifeTracker;

    private void Awake()
    {
        
        Operator.Awake();
    }
    private void Start()
    {
        if (_lifeTracker == null)
        {
            _lifeTracker = FindObjectOfType<LifeTracker>();
        }
        Operator.lifeTracker = _lifeTracker;
        Operator.Start();
    }
    private void Update()
    {
        Operator.Update();
    }
}
