using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State{

    public abstract void GetAction();
    public abstract List<Transition> GetTransitions();

}