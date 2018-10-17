using UnityEngine;
using System.Collections;

public class Face : Align
{
    Agent faceTarget;
    float faceTargetargetRotation = 0f;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        faceTarget = target;
        //target = new Agent();
    }

    // Update is called once per frame
    void Update()
    {
        character.steering.angular = GetSteering().angular;
    }

    public override Steering GetSteering()
    {
        Vector3 direction = faceTarget.transform.position - character.transform.position;

        print(direction);

        if (direction.magnitude==0)
        {
            return steering;
        }

        target = faceTarget;
        faceTargetargetRotation = Mathf.Atan2(-direction.x, direction.z) * Mathf.Rad2Deg;

        print(faceTargetargetRotation);

        return base.GetSteering(faceTargetargetRotation);
    }
}
