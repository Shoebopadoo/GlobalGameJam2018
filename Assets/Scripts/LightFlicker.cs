using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
    public float blinkSpeed;
    Light _light;
	// Use this for initialization
	void Start () {
        _light = GetComponentInChildren<Light>();
        StartCoroutine(Flash());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Flash()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkSpeed);
            _light.enabled = !_light.enabled;
        }
    }
}
