using UnityEngine;
using System.Collections;

public class Jump : GeneralBehaviour
{
    //Point where it can jump
    JumpPoint jumpPoint;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        jumpPoint = new JumpPoint(GameObject.FindGameObjectWithTag("GapStart").transform.position,GameObject.FindGameObjectWithTag("GapEnd").transform.position);

    }

    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    // Update is called once per frame
    public override Steering GetSteering()
    {
        return steering;
    }
}
