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
    void Update()
    {
        character.steering.angular = GetSteering().angular;
    }

    public override Steering GetSteering()
    {
        
        if (character.velocity.magnitude == 0)
        {
            steering.angular = 0f;
            return steering;
        }

        lookTargetRotation = Mathf.Atan2(-character.velocity.x, character.velocity.z) * Mathf.Rad2Deg;

        return base.GetSteering(lookTargetRotation);
    }

}
