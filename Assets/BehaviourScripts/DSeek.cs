using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSeek : GeneralBehaviour {

    // Use this for initialization
    new void Start() {
        base.Start();
        weight = 2f;
    }

    new void Update()
    {
        if(!stop){
            character.SetSteering(GetSteering(), weight, priority);
        }else{
            character.SetSteering(new Steering(), weight, priority);
        }
    }

    public override Steering GetSteering()
    {
        return GetSteeringAux(target.transform.position);
    }

    public Steering GetSteeringAux(Vector3 targetPosition)
    {
        steering.linear = targetPosition - character.transform.position;
        //steering.linear += targetPosition - character.transform.position;
        steering.linear.Normalize();
        steering.linear *= character.maxAcc;
        steering.angular = 0f;
        return steering;
    }


}
