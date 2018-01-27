using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {

    // Jack references
    private Jack _pluggedJack;
    private Jack _targetJack;

    #region Access Variables
    public bool IsFree { get { return _pluggedJack == null; } }
    public bool IsTargetPlugged { get { return _pluggedJack == _targetJack; } }
    public Jack PluggedJack { get { return _pluggedJack; } }
    public Jack Target { get { return _targetJack; } }
    
    #endregion

    // Use this for initialization
    void Start () {
        _pluggedJack = null;
	}
	
	public void PlugIn(Jack target)
    {
        if(IsFree)
        {
            _pluggedJack = target;
            _pluggedJack.PlugIn(this);
        }
    }
    public void Unplug()
    {
        if(_pluggedJack != null)
        {
            _pluggedJack.Unplug();
            _pluggedJack = null;
        }
        
    }

    public void TargetJack(Jack target)
    {
        _targetJack = target;
        _targetJack.Target();
    }


}
