using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;
using UnityEngine;

public class WaitForCall : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        Debug.Log("Entering state: " + this.GetType().ToString());
        // Clear the line targets
        line.Incoming.ClearTarget();
        line.Outgoing.ClearTarget();
    }

    public override void OnExit(PhoneLine line)
    {
        Debug.Log("Leaving state: " + this.GetType().ToString());
    }

    public override void OnUpdate(PhoneLine line)
    {
        // Check if the incoming line has a target
        if (line.Incoming.TargetedJack != null)
        {
            // If so, change state to ringing
            line.ChangeState<Ringing>();
        }
    }
}
public class Ringing : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        Debug.Log("Entering state: " + this.GetType().ToString());
        line.Ring();
    }

    public override void OnExit(PhoneLine line)
    {
        Debug.Log("Leaving state: " + this.GetType().ToString());
        line.StopRinging();
    }

    public override void OnUpdate(PhoneLine line)
    {
        // Check if call is answered
        if(line.Incoming.IsTargetPlugged)
        {
            line.ChangeState<WaitForConnection>();
        }
    }
}

public class WaitForConnection : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        Debug.Log("Entering state: " + this.GetType().ToString());
        line.GetRequest();
    }

    public override void OnExit(PhoneLine line)
    {
        Debug.Log("Leaving state: " + this.GetType().ToString());
    }

    public override void OnUpdate(PhoneLine line)
    {
        if(line.Connected)
        {
            line.ChangeState<InCall>();
        }
    }
}

public class InCall : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        line.startTime = Time.time;
        Debug.Log("Entering state: " + this.GetType().ToString());
    }

    public override void OnExit(PhoneLine line)
    {
        line.endTime = Time.time;
        Debug.Log("Leaving state: " + this.GetType().ToString());
    }

    public override void OnUpdate(PhoneLine line)
    {
        if(Time.time > line.startTime + line.CallLength)
        {
            line.ChangeState<WaitForCall>();
        }
    }
}


public abstract class PhoneState  {
    
    public abstract void OnEnter(PhoneLine line);
    public abstract void OnUpdate(PhoneLine line);
    public abstract void OnExit(PhoneLine line);

    
}
