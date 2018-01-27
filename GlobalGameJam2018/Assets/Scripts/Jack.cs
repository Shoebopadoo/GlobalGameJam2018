using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : MonoBehaviour {

    public int Id;
    private Plug _plug;

    #region Access Variables
    public bool IsFree { get { return _plug == null; } }
    #endregion


    #region Public methods
    // Fill the jack with a plug
    public bool PlugIn(Plug plug)
    {
        if (IsFree)
        {
            _plug = plug;
            return true;
        }
        else
            return false;
    }
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


}
