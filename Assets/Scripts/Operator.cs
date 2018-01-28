using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public static class Operator {

    #region Private Variables
    // Phone line collections
    private static List<PhoneLine> _lines;
    private static List<PhoneLine> _freeLines;

    [SerializeField]
    private static int _lifeCount = 3;
    private static int _roundCount = 0;
    
    // Private fields
    private static float _lastCallTime;
    [SerializeField]
    private static float _minDelay = 7f;
    [SerializeField]
    private static float _maxDelay = 10f;
    private static float _delay;

    private static OperatorState _state;
    public static LifeTracker lifeTracker;
    private static string _scoreScreenPath = "ScoreScreen";
    #endregion


    #region Public Variables
    [SerializeField]
    public static float RoundLength = 30f;  // Seconds
    public static float RoundStart;
    #endregion


    #region Init and Update
    public static void Awake()
    {
        _lines = new List<PhoneLine>();
        _freeLines = new List<PhoneLine>();
    }
    public static void Start()
    {
        _lifeCount = 3;
        ClipManager.LoadClips();
        RandomizeDelay();
        _lastCallTime = Time.time;
    }
    
    public static void Update()
    {
        if (IsCallReady())
        {
            _lastCallTime = Time.time;
            PhoneCall call = RandomCall();
            if(!AssignCall(call))
            {
                PlayerScore.RecordCall(false);
                LoseLife();
            }
        }
    }
    #endregion


    #region Private Methods
    private static bool IsCallReady()
    {
        return Time.time >= _lastCallTime + _delay;
    }
    private static void RandomizeDelay()
    {
        _delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
    }
    private static PhoneCall RandomCall()
    {
        return PhoneCall.RandomCall();
    }

    private static bool AssignCall(PhoneCall call)
    {
        int freeCount = _freeLines.Count;
        if(freeCount == 0)
        {
            _lastCallTime = Time.time;
            return false;
        }
        
        // Choose a random free line
        int idx = UnityEngine.Random.Range(0, freeCount);
        PhoneLine chosenLine = _freeLines[idx];
        chosenLine.ReceiveCall(call);

        // Randomize the delay and reset the calltime
        RandomizeDelay();
        _lastCallTime = Time.time;
        return true;
    }
    private static void GameOver()
    {
        Debug.LogWarning("GAME OVER!");
        SceneManager.LoadScene(_scoreScreenPath);
    }
    #endregion


    #region Public Methods
    public static void LoseLife()
    {
        _lifeCount--;
        lifeTracker.LoseLife();
        if(_lifeCount == 0)
        {
            GameOver();
        }
    }
    public static void RegisterPhoneLine(PhoneLine line)
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
    public static void FreePhoneLine(PhoneLine line)
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
    public static void FillPhoneLine(PhoneLine line)
    {
        if (!_freeLines.Remove(line))
            Debug.LogWarning(line + " not listed as free");
    }
    public static void ChangeState<T>() where T : OperatorState
    {
        // Leave the current state if it is valid
        if (_state != null)
        {
            _state.OnExit();
        }
        // Change the state
        _state = Activator.CreateInstance<T>();
        _state.OnEnter();
    }
    #endregion
}
