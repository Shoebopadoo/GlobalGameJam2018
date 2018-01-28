using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightState { ON, OFF, FLASH}
public class LightController : MonoBehaviour {
    
    public float blinkPeriod = 0.5f;
    Light _light;
    LightState _state = LightState.OFF;

    private float _lastBlink;

	// Use this for initialization
	void Start () {
        _light = GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_state == LightState.FLASH)
        {
            if(Time.time > _lastBlink + blinkPeriod)
            {
                _lastBlink = Time.time;
                _light.enabled = !_light.enabled;
            }
        }
	}
    public void ChangeState(LightState newState)
    {
        _state = newState;
        switch(_state)
        {
            case LightState.ON:
                
                _light.enabled = true;
                break;
            case LightState.OFF:
                _light.enabled = false;
                break;
            case LightState.FLASH:
                _light.enabled = true;
                // Flashing in update
                break;
        }
    }
}
