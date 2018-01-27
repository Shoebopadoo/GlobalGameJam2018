using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlugPositioner : VRTK_InteractableObject {

    
    private Transform startTrans;
    [Header("Custom Options", order = 4)]
    public float speed = 1.0f;

	// Use this for initialization
	void Start ()
    {
        startTrans = transform;
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        Debug.DrawLine(startTrans.position, gameObject.transform.position);
		if (!IsGrabbed() && transform.position != startTrans.position)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, startTrans.position, speed / 100);
        }
	}

}
