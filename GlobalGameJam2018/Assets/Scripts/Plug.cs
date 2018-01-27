using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {
    
    private Jack _jack;

    #region Access Variables
    public bool IsFree { get { return _jack == null; } }
    public Jack GetJack { get { return _jack; } }
    #endregion

    // Use this for initialization
    void Start () {
        _jack = null;
	}
	
	public void PlugIn(Jack target)
    {
        if(IsFree)
        {
            _jack = target;
        }
    }
    public void Unplug()
    {
        _jack = null;
    }
}
