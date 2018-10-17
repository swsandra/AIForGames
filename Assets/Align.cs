using UnityEngine;
using System.Collections;

public class Align : GeneralBehaviour
{
    float tRadius=0.5f, sRadius=1.5f, timeToTarget = 0.1f, angularAcc;
    float rotation, rotationSize;
    float targetRotation;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        character.steering.angular = GetSteering().angular;
        //print(steering.angular);
    }

    public override Steering GetSteering()
    {
        rotation = target.transform.rotation.eulerAngles.z - character.transform.rotation.eulerAngles.z;

        rotation = MapToRange(rotation);
        rotationSize = Mathf.Abs(rotation);

        if (rotationSize < tRadius)
        {
            //steering.linear = Vector3.zero;
            steering.angular = 0;
            return steering;
        }

        if (rotationSize > sRadius)
        {
            targetRotation = character.maxRotation;
        }
        else
        {
            targetRotation = character.maxRotation * rotationSize / sRadius;
        }

        targetRotation = targetRotation * (rotation / rotationSize);

        steering.angular = targetRotation - character.transform.rotation.z;
        steering.angular /= timeToTarget;

        float angularAcc = Mathf.Abs(steering.angular);

        if (angularAcc > character.maxAngularAcc)
        {
            steering.angular /= angularAcc;
            steering.angular *= character.maxAngularAcc;
        }

        //steering.linear = Vector3.zero;

        return steering;
    }


}
