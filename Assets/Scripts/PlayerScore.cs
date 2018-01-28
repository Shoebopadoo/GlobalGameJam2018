using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

    private float _totalScore = 0f;
    private float _multiplier = 1f;
    private float _dMult = .2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(float score)
    {
        _totalScore += score;
    }

    private void UpdateScore()
    {

    }


}
