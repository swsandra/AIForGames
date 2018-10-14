using UnityEngine;
using System.Collections;

public class Align : GeneralBehaviour
{
    float tRadius=0.5f, sRadius=1.5f, timeToTarget = 0.1f, angularAcc;
    float rotation, rotationSize;
    float targetRotation, targetOrientation, characterOrientation;

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
        //rotation = target.orientation - character.orientation;
        //Resta de vectores de posición y lo paso a grados
        Vector3 rotacion = target.transform.position - character.transform.position;
        rotation = Mathf.Atan2(-rotacion.x, rotacion.y) * Mathf.Rad2Deg;

        rotation = MapToRange(rotation);
        rotationSize = Mathf.Abs(rotation);

        if (rotationSize < tRadius)
        {
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

        //print("St ang antes");
        //print(steering.angular);

        steering.angular = targetRotation - character.rotation;
        steering.angular /= timeToTarget;
        
        //print("St ang medio");
        //print(steering.angular);

        float angularAcc = Mathf.Abs(steering.angular);

        if (angularAcc > character.maxAngularAcc)
        {
            steering.angular /= angularAcc;
            steering.angular *= character.maxAngularAcc;
        }

        //print("St ang despues");
        //print(steering.angular);

        return steering;
    }


}
