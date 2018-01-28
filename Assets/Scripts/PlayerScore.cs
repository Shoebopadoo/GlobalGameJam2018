using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PlayerScore {

    private static int _totalCalls = 0;
    private static int _totalConnected = 0;
    private static int _totalMissed = 0;
    private static int _bestStreak = 0;

    private static int _currentStreak = 0;

    #region Access Variables
    public static int TotalCalls { get { return _totalCalls; } }
    public static int TotalConnected { get { return _totalConnected; } }
    public static int TotalMissed { get { return _totalMissed; } }
    public static int BestStreak { get { return _bestStreak; } }
    public static int CurrentStreak { get { return _currentStreak; } }
    #endregion

    // Records a call
    public static void RecordCall(bool wasSuccess)
    {
        _totalCalls++;
        if (wasSuccess){
            _totalConnected++;
            _currentStreak++;
            if (_currentStreak > _bestStreak)
            {
            _bestStreak = _currentStreak;
            }
        }
        else{
            _totalMissed++;
            _currentStreak = 0;
        }   
    }
}


