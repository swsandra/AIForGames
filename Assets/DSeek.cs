using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSeek : GeneralBehaviour {

    // Use this for initialization
    new void Start(){
        base.Start();
    }

    // Update is called once per frame
    //void Update () {
    //    steering.linear = target.transform.position - character.transform.position;
    //    steering.linear.Normalize();
    //    steering.linear *= character.maxAcc;
    //character.steering.angular = 0;
    //}
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {
        steering.linear = target.transform.position - character.transform.position;
        steering.linear.Normalize();
        steering.linear *= character.maxAcc;
        return steering;
    }


}
