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
    void Update()
    {
        character.steering.angular = GetSteering(faceTarget.transform.position).angular;
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
        faceTargetRotation = Mathf.Atan2(-direction.x, direction.z) * Mathf.Rad2Deg;
        //print(faceTargetRotation);

        return base.GetSteering(faceTargetRotation);
    }
}
