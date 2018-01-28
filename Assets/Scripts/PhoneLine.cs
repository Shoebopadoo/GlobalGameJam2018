﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneLine : MonoBehaviour {
    [SerializeField]
    private int _lineNum;
    [SerializeField]
    private Switchboard _board; // Switchboard
    [SerializeField]
    private Operator _operator; // Operator
    [SerializeField]
    private Plug _outgoing;     // Outgoing plug
    [SerializeField]
    private Plug _incoming;     // Incoming plug
    [SerializeField]
    private float _callLength = 10f;  // 10 second calls to start with

    [HideInInspector]
    public float startTime;
    [HideInInspector]
    public float endTime;

    private PhoneState _state;
    private PhoneCall _currCall;
    private AudioSource _audioSource;

    #region Access Variables
    public Plug Outgoing { get { return _outgoing; } }
    public Plug Incoming { get { return _incoming; } }
    public bool IsConnected { get { return _incoming.IsTargetPlugged && _outgoing.IsTargetPlugged; } }
    public bool IsUnplugged { get { return _outgoing.IsFree && Incoming.IsFree; } }
    public float CallLength { get { return _callLength; } }
    public Type State { get { return _state.GetType(); } }
    public Operator LineOperator { get { return _operator; } }


    #endregion


    #region Unity Callbacks
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        _operator.RegisterPhoneLine(this);
        ChangeState<WaitForCall>();
	}
	
	// Update is called once per frame
	void Update () {
        _state.OnUpdate(this);
	}
    #endregion

    // Get an incoming call
    public void ReceiveCall(PhoneCall call)
    {
        // Check you are waiting for calls
        if(_state.GetType() == typeof(WaitForCall))
        {
            Jack inJack = _board.FindFreeJack();
            Debug.Log("Incoming call on jack " + inJack.Id);

            if(call != null)
            {
                _currCall = call;
                _audioSource.clip = _currCall.DialogClip;
                _incoming.Target(inJack);
                _operator.FillPhoneLine(this);
            }
        }
    }

    // Play the call sound
    public void PlayCall()
    {
        _audioSource.Play();
    }
    public void ClearLine()
    {
        _currCall = null;
        _outgoing.ClearTarget();
        _incoming.ClearTarget();
        _operator.FreePhoneLine(this);
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



