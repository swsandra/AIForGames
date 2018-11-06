using UnityEngine;
using System.Collections;

public class VelocityMatch : GeneralBehaviour
{
    float timeToTarget = 0.1f;
    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    // Update is called once per frame
    public override Steering GetSteering()
    {
        steering.linear = target.velocity - character.velocity;
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
