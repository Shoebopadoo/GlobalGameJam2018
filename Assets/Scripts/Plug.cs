using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {

    // Jack references
    private Jack _pluggedJack;
    private Jack _targetJack;

    #region Access Variables
    public bool IsFree { get { return _pluggedJack == null; } }
    public bool IsTargetPlugged { get {
            if (_targetJack == null || _pluggedJack == null) return false;
            else return _targetJack == _pluggedJack; } }
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
    }
    public void Unplug()
    {
        if (IsTargetPlugged)
        {
            ClearTarget();
        }
        _pluggedJack = null;
    }

    public void Target(Jack target)
    {
        Debug.Log("Connect to " + target.name);
        _targetJack = target;
        _targetJack.Target();
    }
    public void ClearTarget()
    {
        if(_targetJack != null)
            _targetJack.Untarget();
        _targetJack = null;
    }
}
