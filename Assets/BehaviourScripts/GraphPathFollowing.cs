using UnityEngine;
using System.Collections;

public class GraphPathFollowing : DSeek
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    public override Steering GetSteering()
    {
        

        return base.GetSteering();
    }
}