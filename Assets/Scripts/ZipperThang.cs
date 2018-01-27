using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipperThang : MonoBehaviour {
    private Vector3 goal = new Vector3(0, 1, 0);
    public bool grabbed = true;
    public float speed = 1.0F;
    private float journeyLength;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position != goal && grabbed == false)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, goal, speed / 100);
        }
	}
}