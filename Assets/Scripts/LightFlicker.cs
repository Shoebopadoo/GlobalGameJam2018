using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    Light _light;
	// Use this for initialization
	void Start () {
        _light = GetComponent<Light>();
        StartCoroutine(Flash());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Flash()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _light.enabled = !_light.enabled;
        }
    }
}
