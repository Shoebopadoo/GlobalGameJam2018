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
    private float _minDelay = 3f;
    [SerializeField]
    private float _maxDelay = 10f;
    private float _delay;

    private void Start()
    {
        foreach(PhoneLine line in _lines)
        {
            _freeLines.Add(line);
        }
        // Randomize the delay between calls
        _delay = Random.Range(_minDelay, _maxDelay);
        _lastCallTime = Time.time - _delay;
    }

    private void Update()
    {
        if(IsCallReady())
        {
            PhoneCall call = RandomCall();
            AssignCall(call);
        }
    }

    // Checks if call is ready
    private bool IsCallReady()
    {
        return Time.time >= _lastCallTime + _delay;
    }
    
    public void AssignCall(PhoneCall call)
    {
        int freeCount = _freeLines.Count;
        if(freeCount == 0)
        {
            Debug.LogWarning("No free lines");
            return;
        }
        
        // Choose a random free line
        int idx = Random.Range(0, freeCount);
        PhoneLine chosenLine = _freeLines[idx];

        chosenLine.ReceiveCall(call);
    }
    private PhoneCall RandomCall()
    {
        return PhoneCall.RandomCall();
    }
    public void RegisterPhoneLine(PhoneLine line)
    {
        // Add to master list
        if(!_lines.Contains(line))
        {
            _lines.Add(line);
        }
        // Add to free lines
        if(!_freeLines.Contains(line))
        {
            _lines.Add(line);
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
    
}
