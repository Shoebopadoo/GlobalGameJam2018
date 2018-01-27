using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Text;
using UnityEngine;


public abstract class PhoneState  {
    
    public abstract void OnEnter(PhoneLine line);
    public abstract void OnUpdate(PhoneLine line);
    public abstract void OnExit(PhoneLine line);

    public static void ChangeState<T>(PhoneLine line, PhoneState state) where T : PhoneState
    {
        state.OnExit(line);
        state = Activator.CreateInstance<T>();
        state.OnEnter(line);
    }
}
