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
        character.steering.linear = GetSteering().linear;
    }

    public override Steering GetSteering()
    {
        character.steering.linear = character.transform.position - target.transform.position;
        character.steering.linear.Normalize();
        character.steering.linear *= character.maxAcc;
        return steering;
    }

}
