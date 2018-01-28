using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlugPositioner : VRTK_InteractableObject {

    
    private Transform startTrans;
    private Vector3 startPos;
    private Vector3 startRot;
    private bool _isLerping;
    private bool isSnapped;

    [Header("Custom Options", order = 4)]
    public float speed = 1.0f;
    public bool grabbed = true;
    public float snapValue = 1.0f;

	// Use this for initialization
	void Start ()
    {
        startRot = transform.rotation.eulerAngles;
        startPos = transform.position;
        print("Position:" + startPos);
        print("Rotation: " + startRot);
        
	}
	
	// Update is called once per frame
	protected override void Update ()
    {

        if (grabbed != true && !IsInSnapDropZone())
        {
            if (transform.position != startPos || transform.rotation.eulerAngles != startRot)
            {
               // gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //transform.position = Vector3.Lerp(gameObject.transform.position, startTrans.position, speed / 100);
                transform.position = Vector3.Lerp(gameObject.transform.position, startPos, speed / 100);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(startRot), speed / 100);
                if (Vector3.Distance(transform.position, startPos) < snapValue)
                {
                    transform.position = startPos;
                    transform.rotation = Quaternion.Euler(startRot);
                }

            }
        }
        
    }

    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
    {
        grabbed = true;
        base.Grabbed(currentGrabbingObject);
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
    {
        grabbed = false;
        base.Ungrabbed(previousGrabbingObject);
    }  
}
