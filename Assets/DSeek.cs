using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSeek : GeneralBehaviour {

    // Use this for initialization
    new void Start() {
        base.Start();
    }

    void Update()
    {
        character.steering.linear = GetSteering(target.transform.position).linear;
    }

    public override Steering GetSteering(Vector3 targetPosition)
    {
        steering.linear = targetPosition - character.transform.position;
        //steering.linear += targetPosition - character.transform.position;
        steering.linear.Normalize();
        steering.linear *= character.maxAcc;
        return steering;
    }


}
