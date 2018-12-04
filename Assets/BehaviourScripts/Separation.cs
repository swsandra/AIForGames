using UnityEngine;
using System.Collections;

public class Separation : GeneralBehaviour
{
    public Agent[] targets;
    float threshold=6f, decayCoefficient=10f;
    // Use this for initialization
    new void Start()
    {
        base.Start();
        weight = 1.5f;
    }

    // Update is called once per frame
    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    public override Steering GetSteering()
    {

        foreach (Agent aTarget in targets)
        {
            //Vector3 direction = target.transform.position - character.transform.position; //Esto hace algo loco
            Vector3 direction = character.transform.position - aTarget.transform.position;
            float distance = direction.magnitude;
            
            float strength;
            if (distance < threshold)
            {
                strength = Mathf.Min(decayCoefficient/(distance*distance), character.maxAcc);
                direction.Normalize();
                steering.linear += strength * direction;
            }
            else
            {
                steering.linear = Vector3.zero;
            }

        }
        steering.angular = 0f;
        return steering;
    }

}
