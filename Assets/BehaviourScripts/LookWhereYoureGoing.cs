using UnityEngine;
using System.Collections;

public class LookWhereYoureGoing : Align
{

    float lookTargetRotation;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    public override Steering GetSteering()
    {
        
        if (character.velocity.magnitude == 0)
        {
            steering.angular = 0f;
            steering.linear = Vector3.zero;
            return steering;
        }

        lookTargetRotation = Mathf.Atan2(-character.velocity.x, character.velocity.y) * Mathf.Rad2Deg;

        steering.linear = Vector3.zero;
        return base.GetSteeringAux(lookTargetRotation);
    }

}
