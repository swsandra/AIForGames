using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class BehaviourAndWeight
{
    public GameObject behaviour;
    public float weight;

    public BehaviourAndWeight()
    {
        behaviour = null;
        weight = 0f;
    }

    public BehaviourAndWeight(GameObject behav, float we)
    {
        behaviour = behav;
        weight = we;
    }
}
