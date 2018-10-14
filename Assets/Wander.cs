using UnityEngine;
using System.Collections;

public class Wander : Face
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Steering GetSteering()
    {


        return base.GetSteering();
    }

}
