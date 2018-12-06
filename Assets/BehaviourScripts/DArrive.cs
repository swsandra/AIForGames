using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DArrive : GeneralBehaviour {

    //Target radius, slow radius, time to target, distance
    public float tRadius = 1f, sRadius = 3f, timeToTarget = 0.3f, distance;
    public Vector3 tVelocity;
    public float tSpeed;

    public Vector3 direction;

    // Use this for initialization
    new void Start(){
        base.Start();
        direction = Vector3.zero;
        tVelocity = Vector3.zero;
        distance = 0.0f;
    }

    // Update is called once per frame
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
        direction = target.transform.position - character.transform.position; 
        distance = direction.magnitude; 

        if (distance < tRadius)
        {
            steering.linear = Vector3.zero; 
            character.velocity = Vector3.zero;    
            return steering;
        }

        if (distance > sRadius)
        {
            tSpeed = character.maxSpeed;
        }
        else
        {
            tSpeed = character.maxSpeed * (distance / sRadius);
        }
        tVelocity = direction.normalized * tSpeed;
        //tVelocity *= tSpeed;

        steering.linear = tVelocity - character.velocity;
        
        steering.linear /= timeToTarget;

        if (steering.linear.magnitude > character.maxAcc)
        {
            steering.linear.Normalize();
            steering.linear *= character.maxAcc;
        }

        steering.angular = 0f;

        return steering;
    }
}
