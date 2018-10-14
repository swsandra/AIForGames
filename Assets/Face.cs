using UnityEngine;
using System.Collections;

public class Face : Align
{
    Agent faceTarget;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        faceTarget = target;
        target = new Agent();
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {
        Vector3 direction = faceTarget.transform.position - character.transform.position;

        if (direction.magnitude==0)
        {
            return steering;
        }

        target = faceTarget;
        target.orientation = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

        return base.GetSteering();
    }
}
