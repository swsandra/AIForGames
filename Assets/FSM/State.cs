using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State{

    public abstract void MakeAction();

    public abstract void MakeEntryAction();

    public abstract void MakeExitAction();

    public abstract List<Transition> GetTransitions();

}