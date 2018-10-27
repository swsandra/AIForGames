using UnityEngine;
using System.Collections;

public class Separation : GeneralBehaviour
{
    public Agent[] targets;
    float threshold=4f, decayCoefficient=5f;
    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {

        foreach (Agent target in targets)
        {
            //Vector3 direction = target.transform.position - character.transform.position; //Esto hace algo loco
            Vector3 direction = character.transform.position - target.transform.position;
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
                character.velocity = Vector3.zero;
                steering.linear = Vector3.zero;
            }

        }

        return steering;
    }

}
