using UnityEngine;
using System.Collections;

public class Face : Align
{
    Agent faceTarget;
    float faceTargetRotation = 0f;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        faceTarget = target;
        target = new Agent();
    }

    // Update is called once per frame
    new void Update()
    {
        character.SetSteering(GetSteering(faceTarget.transform.position), weight, priority);
    }

    public Steering GetSteering(Vector3 targetDirection)
    {
        //Vector3 direction = faceTarget.transform.position - character.transform.position; //With this line and without parameters, works
        Vector3 direction = targetDirection - character.transform.position;

        //print(direction);

        if (direction.magnitude==0f)
        {
            steering.angular = 0f;
            return steering;
        }

        target = faceTarget;
        faceTargetRotation = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
        //print(faceTargetRotation);

        steering.linear = Vector3.zero;

        return base.GetSteering(faceTargetRotation);
    }
}
