using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Transition{

    public abstract bool IsTriggered();

    public abstract State GetTargetState();

}