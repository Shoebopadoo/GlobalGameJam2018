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
    
	
    public void LoseLife()
    {
        if(_third.enabled == true)
        {
            _third.color = Color.red;
        }
        else if (_second.enabled == true)
        {
            _second.color = Color.red;
        }
        else if (_first.enabled == true)
        {
            _first.color = Color.red;
        }
    }
}
