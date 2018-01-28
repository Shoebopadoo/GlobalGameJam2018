using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator : MonoBehaviour {
    
    // Phone line collections
    private List<PhoneLine> _lines;
    private List<PhoneLine> _freeLines;

    // Private fields
    private float _lastCallTime;
    [SerializeField]
    private float _minDelay = 5f;
    [SerializeField]
    private float _maxDelay = 10f;
    private float _delay;

    #region Unity Callbacks
    private void Awake()
    {
        _lines = new List<PhoneLine>();
        _freeLines = new List<PhoneLine>();
    }
    private void Start()
    {
        ClipManager.LoadClips();
        RandomizeDelay();
        _lastCallTime = Time.time;
    }
    private void Update()
    {
        if (IsCallReady())
        {
            PhoneCall call = RandomCall();
            AssignCall(call);
        }
    }
    #endregion


    #region Private Methods
    private bool IsCallReady()
    {
        return Time.time >= _lastCallTime + _delay;
    }
    private void RandomizeDelay()
    {
        _delay = Random.Range(_minDelay, _maxDelay);
    }
    private PhoneCall RandomCall()
    {
        return PhoneCall.RandomCall();
    }
    
    private void AssignCall(PhoneCall call)
    {
        int freeCount = _freeLines.Count;
        if(freeCount == 0)
        {
            //Debug.LogWarning("No free lines");
            return;
        }
        
        // Choose a random free line
        int idx = Random.Range(0, freeCount);
        PhoneLine chosenLine = _freeLines[idx];
        chosenLine.ReceiveCall(call);

        // Randomize the delay and reset the calltime
        RandomizeDelay();
        _lastCallTime = Time.time;
    }
    
    public void RegisterPhoneLine(PhoneLine line)
    {
        Debug.Log("attempting to register line: " + line);
        // Add to master list
        if(!_lines.Contains(line))
        {
            Debug.Log("Adding line to master list: " + line);
            _lines.Add(line);
        }
        // Add to free lines
        if(!_freeLines.Contains(line))
        {
            Debug.Log("Adding line to free list: " + line);
            _freeLines.Add(line);
        }
    }
    public void FreePhoneLine(PhoneLine line)
    {
        if(!_freeLines.Contains(line))
        {
            _freeLines.Add(line);
        }
        else
        {
            Debug.LogWarning(line.name + " already free");
        }
    }
    public void FillPhoneLine(PhoneLine line)
    {
        if (!_freeLines.Remove(line))
            Debug.LogWarning(line + " not listed as free");
    }
    #endregion
}
