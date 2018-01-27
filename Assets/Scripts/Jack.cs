using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

enum JackState { EMPTY, FULL, TARGET}
public class Jack : MonoBehaviour {

    public int Id;
    private bool _targeted = false;

    private Plug _plug;
    [SerializeField]
    private Switchboard _board;
    private VRTK_SnapDropZone _snapDropZone;

    #region Access Variables
    public bool IsFree { get { return _plug == null; } }
    public bool IsTargeted {  get { return _targeted; } }
    public bool GetPlug { get { return _plug; } }
    public Switchboard Board { get { return _board; } }
    #endregion


    #region Unity Callbacks
    // Use this for initialization
    void Start () {
        // Set jack name
        gameObject.name = "Jack_" + Id;
        _board.RegisterJack(this);

        // Subscribe to snapzone events
        _snapDropZone = GetComponentInChildren<VRTK_SnapDropZone>();
        _snapDropZone.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(OnSnap);
        _snapDropZone.ObjectUnsnappedFromDropZone += new SnapDropZoneEventHandler(OnUnsnap);
        
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
    // Unplug the plug from the jack and return it
    public Plug Unplug()
    {
        Plug tPlug = _plug;
        _plug = null;
        tPlug.Unplug();
        _board.FreeJack(this);
        Debug.Log("Unplugged " + tPlug + " from jack " + Id);
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

    // Called when an object is snapped in
    public void OnSnap(object sender, SnapDropZoneEventArgs e)
    {
        Plug snappedPlug = e.snappedObject.GetComponent<Plug>();
        if(!snappedPlug)
        {
            Debug.LogError("Snapped obj not a plug");
            return;
        }
        // Try to plug it in
        if(!PlugIn(snappedPlug))
        {
            Debug.LogError("Unable to snap plug in");
            _snapDropZone.ForceUnsnap();
        }
    }
    public void OnUnsnap(object sender, SnapDropZoneEventArgs e)
    {
        Plug snappedPlug = e.snappedObject.GetComponent<Plug>();
        if (!snappedPlug)
        {
            Debug.LogError("Snapped obj not a plug");
            return;
        }
        // Unplug
        Unplug();
    }
    #endregion
    

}
