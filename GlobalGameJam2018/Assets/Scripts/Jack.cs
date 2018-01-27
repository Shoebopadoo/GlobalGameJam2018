using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JackState { EMPTY, FULL, TARGET}
public class Jack : MonoBehaviour {

    public int Id;
    private bool _targeted = false;

    private Plug _plug;
    [SerializeField]
    private Switchboard _board;

    #region Access Variables
    public bool IsFree { get { return _plug == null; } }
    public bool IsTargeted {  get { return _targeted; } }
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
            _board.FillJack(this);
            return true;
        }
        else
            return false;
    }
    // Unplug the plug from the jack and return it
    public Plug Unplug()
    {
        Plug tPlug = _plug;
        _plug = null;
        _board.FreeJack(this);
        return tPlug;
    }

    public void Target()
    {
        if(!_targeted)
        {
            _targeted = true;
            _board.TargetJack(this);
        }
    }
    public void Untarget()
    {
        if(_targeted)
        {
            _targeted = false;
            _board.UntargetJack(this);
        }
    }
    #endregion
    

}
