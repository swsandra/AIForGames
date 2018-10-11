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

    void Update()
    {
        character.steering = GetSteering();
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

        return steering;

    }
}
