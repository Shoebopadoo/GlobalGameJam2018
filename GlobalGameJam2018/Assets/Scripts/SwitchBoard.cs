using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchboard : MonoBehaviour {

    private List<Jack> _jacks;
    private List<Jack> _emptyJacks;

    #region Access Variables
    public List<Jack> Jacks { get { return _jacks; } }
    public List<Jack> EmptyJacks { get { return _emptyJacks; } }
    #endregion


    #region Unity Callbacks
    private void Start()
    {
        _jacks = new List<Jack>();
        _emptyJacks = new List<Jack>();
    }
    #endregion

    public bool FillJack(Jack target, Plug plug)
    {
        if (target.IsFree)
        {
            target.PlugIn(plug);
            return true;
        }
        else
        {
            return false;
        }
    }

}
