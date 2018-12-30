using UnityEngine;
using System.Collections;

public abstract class Transition{

    public abstract bool IsTriggered();

    public abstract State GetTargetState();

}