using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipperThang : MonoBehaviour {
    private Transform startPos;
    private Vector3 goal = new Vector3(0, 1, 0);
    public bool grabbed = true;
    public float speed = 1.0F;
    private float journeyLength;

    // Use this for initialization
    void Start ()
    {
        startPos = transform;
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position != startPos.position && grabbed == false)
        {

            transform.position = Vector3.Lerp(gameObject.transform.position, goal, speed / 100);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), speed / 100);

            transform.position = Vector3.Lerp(gameObject.transform.position, startPos.position, speed / 100);
            //transform.eulerAngles = Vector3.Lerp(gameObject.transform.rotation.eulerAngles, startPos.rotation.eulerAngles, speed / 100);

        }
	}
}