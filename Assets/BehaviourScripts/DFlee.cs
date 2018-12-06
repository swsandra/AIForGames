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
        if(!stop){
            character.SetSteering(GetSteering(), weight, priority);
        }else{
            character.SetSteering(new Steering(), weight, priority);
        }
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
