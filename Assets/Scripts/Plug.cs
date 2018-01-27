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
    public Jack TargetedJack { get { return _targetJack; } }
    
    #endregion

    // Use this for initialization
    void Start () {
        _pluggedJack = null;
	}
	
	public void PlugIn(Jack target)
    {
        _pluggedJack = target;
        if(IsTargetPlugged)
        {
            ClearTarget();
        }
    }
    public void Unplug()
    {
        _pluggedJack = null;
    }

    public void Target(Jack target)
    {
        _targetJack = target;
        _targetJack.Target();
    }
    public void ClearTarget()
    {
        _targetJack = null;
    }


}
