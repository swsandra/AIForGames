using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Transition{

    public bool IsTriggered();

    public State GetTargetState();

}