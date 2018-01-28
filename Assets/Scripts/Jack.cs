using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

enum JackState { EMPTY, FULL, TARGET}
public class Jack : MonoBehaviour
{


    public int Id;
    private bool _targeted = false;

    private Plug _plug;
    [SerializeField]
    private Switchboard _board;
    LightController _lightControl;
    private VRTK_SnapDropZone _snapDropZone;

    #region Access Variables
    public bool IsFree { get { return _plug == null; } }
    public bool IsTargeted { get { return _targeted; } }
    public bool GetPlug { get { return _plug; } }
    public Switchboard Board { get { return _board; } }
    #endregion


    #region Unity Callbacks
    // Use this for initialization
    void Start()
    {
        // Set jack name
        gameObject.name = "Jack_" + Id;
        if(!Board)
        {
            GameObject goBoard = GameObject.Find("switchboard");
            Switchboard brd = goBoard.GetComponent<Switchboard>();
            if(brd)
            {
                _board = brd;
            }
        }
        _board.RegisterJack(this);
        _lightControl = GetComponent<LightController>();

        // Subscribe to snapzone events
        _snapDropZone = GetComponentInChildren<VRTK_SnapDropZone>();
        _snapDropZone.ObjectSnappedToDropZone += new SnapDropZoneEventHandler(OnSnap);
        _snapDropZone.ObjectUnsnappedFromDropZone += new SnapDropZoneEventHandler(OnUnsnap);

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion


    #region Public methods
    // Fill the jack with a plug
    public bool PlugIn(Plug plug)
    {
        _lightControl.ChangeState(LightState.ON);
        if (IsFree)
        {
            _plug = plug;
            _plug.PlugIn(this);
            Debug.Log("Plugged " + _plug + " into " + gameObject.name);
            return true;
        }
        else
            return false;
    }
    // Unplug the plug from the jack and return it
    public Plug Unplug()
    {
        // Change the light state
        if (_targeted)
            _lightControl.ChangeState(LightState.FLASH);
        else
            _lightControl.ChangeState(LightState.OFF);
        
        Plug tPlug = _plug;
        _plug = null;
        if(tPlug != null)
        {
            tPlug.Unplug();
            _board.FreeJack(this);
            Debug.Log("Unplugged " + tPlug + " from " + gameObject.name);
        }
        return tPlug;
    }

    public void Target()
    {
        if (!_targeted)
        {
            _targeted = true;
            _board.TargetJack(this);
            _lightControl.ChangeState(LightState.FLASH);
        }
    }
    public void Untarget()
    {
        if (_targeted)
        {
            _targeted = false;
            _board.UntargetJack(this);
            _lightControl.ChangeState(LightState.OFF);
        }
    }

    // Called when an object is snapped in
    public void OnSnap(object sender, SnapDropZoneEventArgs e)
    {
        Plug snappedPlug = e.snappedObject.GetComponent<Plug>();
        if (!snappedPlug)
        {
            Debug.LogError("Snapped obj not a plug");
            return;
        }
        // Try to plug it in
        if (!PlugIn(snappedPlug))
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
            Debug.LogError("Unsnapped obj not a plug");
            return;
        }
        // Unplug
        Unplug();
    }
    #endregion


    #region Private Methods
    private void Highlight()
    {
        
    }
    private void UnHighlight()
    {

    }
    #endregion

}
