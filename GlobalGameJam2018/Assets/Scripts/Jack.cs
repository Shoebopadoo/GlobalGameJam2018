using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : MonoBehaviour {

    public int Id;
    private Plug _plug;
    [SerializeField]
    private Switchboard _board;

    #region Access Variables
    public bool IsFree { get { return _plug == null; } }
    public bool GetPlug { get { return _plug; } }
    public Switchboard Board { get { return _board; } }
    #endregion


    #region Unity Callbacks
    // Use this for initialization
    void Start () {
        _plug = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion


    #region Public methods
    // Fill the jack with a plug
    public bool PlugIn(Plug plug)
    {
        if (IsFree)
        {
            _plug = plug;
            _plug.PlugIn(this);
            _board.FillJack(this);
            return true;
        }
        else
            return false;
    }
    // Unplug the plug from the jack
    public Plug Unplug()
    {
        Plug tPlug = _plug;
        _plug = null;
        tPlug.Unplug();
        _board.FreeJack(this);
        return tPlug;
    }
    #endregion
    

}
