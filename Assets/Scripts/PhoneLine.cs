using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneLine : MonoBehaviour {
    [SerializeField]
    private int _lineNum;
    [SerializeField]
    private Switchboard _board;
    [SerializeField]
    private Plug _outgoing;
    [SerializeField]
    private Plug _incoming;
    [SerializeField]
    private float _callLength = 10f;  // 10 second calls to start with
    
    [HideInInspector]
    public float startTime;
    [HideInInspector]
    public float endTime;

    private PhoneState _state;
    #region Access Variables
    public Plug Outgoing { get { return _outgoing; } }
    public Plug Incoming { get { return _incoming; } }
    public bool Connected { get { return _incoming.IsTargetPlugged && _outgoing.IsTargetPlugged; } }
    public float CallLength { get { return _callLength; } }
    #endregion


    #region Unity Callbacks
    // Use this for initialization
    void Start () {
        ChangeState<WaitForCall>();
	}
	
	// Update is called once per frame
	void Update () {
        _state.OnUpdate(this);
	}
    #endregion

    // Get an incoming call
    public void ReceiveCall()
    {
        // Check you are waiting for calls
        if(_state.GetType() == typeof(WaitForCall))
        {
            Jack inJack = _board.FindFreeJack();
            Debug.Log("Incoming call on jack " + inJack.Id);
            _incoming.Target(inJack);
        }
        
    }

    // Changes the phone state
    public void ChangeState<T>() where T : PhoneState
    {
        // Leave the current state if it is valid
        if(_state != null)
        {
            _state.OnExit(this);
        }
        // Change the state
        _state = Activator.CreateInstance<T>();
        _state.OnEnter(this);
    }

    public void Ring()
    {
        // Start ringing sound & light up
        Debug.Log("Ring Ring!");
    }
    public void StopRinging()
    {
        // Cancel ringing sound & clear light
        Debug.Log("Ringing stopped");
    }

    // Get a request to connect to a line
    public void GetRequest()
    {
        Jack tJack = _board.FindFreeJack();
        _outgoing.Target(tJack);
    }
}



