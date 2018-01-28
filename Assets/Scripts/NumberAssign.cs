using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumberAssign : MonoBehaviour
{
    public Text myTextobj;
    public int myID;

	// Use this for initialization
	void Start ()
    {
        myID = gameObject.GetComponent<Jack>().Id;
        myTextobj = gameObject.GetComponentInChildren<Text>();
        myTextobj.text = myID.ToString();
	}
}
