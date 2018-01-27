using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneLine : MonoBehaviour {

    private int _lineNum;
    private Switchboard _board;
    private Plug _outgoing;
    private Plug _incoming;


    #region Access Variables
    public Plug Outgoing { get { return _outgoing; } }
    public Plug Incoming { get { return _incoming; } }
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IncomingCall()
    {
        Jack inJack = _board.FindFreeJack();
        Debug.Log("Incoming call on jack " + inJack.Id);
        _incoming.TargetJack(inJack);
    }

}



