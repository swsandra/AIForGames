using UnityEngine;
using System.Collections;

public class Evade : DFlee
{

    Agent pursueTarget;
    float maxPrediction, speed, prediction;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        pursueTarget = target;
        target = new Agent();
        maxPrediction = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {
        Vector3 direction = pursueTarget.transform.position - character.transform.position;
        float distance = direction.magnitude;

        speed = character.velocity.magnitude;

        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        target = pursueTarget;

        target.transform.position += pursueTarget.velocity * prediction;

        return base.GetSteering();
    }
}
