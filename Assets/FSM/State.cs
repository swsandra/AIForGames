using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class State{

    public void MakeAction();

    public void MakeEntryAction();

    public void MakeExitAction();

    public List<Transition> GetTransitions();

}