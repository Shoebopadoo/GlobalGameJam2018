using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsUI : MonoBehaviour {

    [SerializeField]
    Text txtTotal;
    [SerializeField]
    Text txtComplete;
    [SerializeField]
    Text txtMissed;
    [SerializeField]
    Text txtStreak;

    public void Start()
    {
        txtTotal.text = "Total: " + PlayerScore.TotalCalls;
        txtComplete.text = "Completed: " + PlayerScore.TotalConnected;
        txtMissed.text = "Missed: " + PlayerScore.TotalMissed;
        txtStreak.text = "Best Streak: " + PlayerScore.BestStreak;
    }


}
