using UnityEngine;
using System.Collections;

[System.Serializable]
public class BehaviourAndWeight
{
    [SerializeField] public GeneralBehaviour behaviour;
    public float weight;

    public BehaviourAndWeight()
    {
        behaviour = null;
        weight = 0f;
    }

}
