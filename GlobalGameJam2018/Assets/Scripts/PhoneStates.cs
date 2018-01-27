using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhoneState  {

    public abstract void OnEnter(PhoneLine line);
    public abstract void OnUpdate(PhoneLine line);
    public abstract void OnExit(PhoneLine line);

    
}
