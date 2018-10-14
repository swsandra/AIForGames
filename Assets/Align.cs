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
        //character.orientation = Mathf.Atan2(-character.transform.position.x, character.transform.position.y) * Mathf.Rad2Deg; // Cambie z por y
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {
        rotation = target.orientation - character.orientation;
        rotation = MapToRange(rotation);
        rotationSize = Mathf.Abs(rotation);

        if (rotationSize < tRadius)
        {
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

        targetRotation *= rotation / rotationSize;

        character.steering.angular = targetRotation - character.rotation;
        character.steering.angular /= timeToTarget;

        float angularAcc = Mathf.Abs(character.steering.angular);

        if (angularAcc > character.maxAngularAcc)
        {
            character.steering.angular /= angularAcc;
            character.steering.angular *= character.maxAngularAcc;
        }
        return steering;
    }


}
