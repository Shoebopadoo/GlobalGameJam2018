using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;
using UnityEngine;

// Waiting for a call
public class WaitForCall : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        Debug.Log("Entering state: " + this.GetType().ToString());
        line.ChangeLight(LightState.OFF);
        // Clear the line targets
        line.ClearLine();
    }

    public override void OnExit(PhoneLine line)
    {
        Debug.Log("Leaving state: " + this.GetType().ToString());
        Operator.FillPhoneLine(line);
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

// Phone ringing
public class Ringing : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        Debug.Log("Entering state: " + this.GetType().ToString());
        line.ChangeLight(LightState.FLASH);
        line.ringTime = Time.time;
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
        else if(Time.time > line.ringTime + line.ringLength)
        {
            line.CallDropped();
        }
    }
}

// Waiting for connection
public class WaitForConnection : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        Debug.Log("Entering state: " + this.GetType().ToString());
        line.PlayCall();
        line.GetRequest();
        line.ChangeLight(LightState.ON);
        line.startTime = Time.time;
    }

    public override void OnExit(PhoneLine line)
    {
        Debug.Log("Leaving state: " + this.GetType().ToString());
    }

    public override void OnUpdate(PhoneLine line)
    {
        if (Time.time >= line.startTime + line.CallLength)
        {
            line.CallDropped();
        }
        else if (line.IsConnected)
        {
            line.ChangeState<InCall>();
        }
        
    }
}

// In a phone call
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
        if (!line.IsConnected)
        {
            Debug.LogWarning("Disconnected too early!");
            line.CallDropped();
        }

        if(Time.time > line.startTime + line.CallLength)
        {
            line.ChangeState<CallComplete>();
        }
    }

}

public class CallComplete : PhoneState
{
    public override void OnEnter(PhoneLine line)
    {
        PlayerScore.RecordCall(true);
        line.ChangeLight(LightState.OFF);
        Debug.Log("Entering state: " + this.GetType().ToString());
    }

    public override void OnExit(PhoneLine line)
    {
        Debug.Log("Leaving state: " + this.GetType().ToString());
    }

    public override void OnUpdate(PhoneLine line)
    {
        if(line.IsUnplugged)
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
