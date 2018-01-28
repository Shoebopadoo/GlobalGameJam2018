using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTracker : MonoBehaviour {

    [SerializeField]
    Light _first;
    [SerializeField]
    Light _second;
    [SerializeField]
    Light _third;

    private void Start()
    {
        _first.enabled = true;
        _second.enabled = true;
        _third.enabled = true;
    }

    public void LoseLife()
    {
        if(_third.color != Color.red)
        {
            _third.color = Color.red;
        }
        else if (_second.color != Color.red)
        {
            _second.color = Color.red;
        }
        else if (_first.color != Color.red)
        {
            _first.color = Color.red;
        }
    }
}
