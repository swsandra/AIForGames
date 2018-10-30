using UnityEngine;
using System.Collections;

public class DFlee : GeneralBehaviour
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        //character.steering.linear = GetSteering().linear;
        character.SetSteering(GetSteering(), weight);
    }

    public override Steering GetSteering()
    {
        steering.linear = character.transform.position - target.transform.position;
        steering.linear.Normalize();
        steering.linear *= character.maxAcc;
        steering.angular = 0f;
        return steering;
    }

}
