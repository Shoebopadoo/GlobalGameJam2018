using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base state class
public abstract class OperatorState {
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

// Break
public class Break : OperatorState
{
    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
// Active
public class Active : OperatorState
{
    public override void OnEnter()
    {
        //Debug.Log("Active");
        Operator.RoundStart = Time.time;
    }

    public override void OnExit()
    {
        //Debug.Log("Inactive");
    }

    public override void OnUpdate()
    {
        if(Time.time > Operator.RoundStart + Operator.RoundLength)
        {
            Operator.ChangeState<Break>();
        }
    }
}