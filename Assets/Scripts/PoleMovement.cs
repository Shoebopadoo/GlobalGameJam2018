using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleMovement : MonoBehaviour {
    public GameObject Object1;
    public GameObject Object2;
   
	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<LineRenderer>().SetPosition(0, Object1.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, Object2.transform.position);
    }
}
