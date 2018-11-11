using UnityEngine;
using System.Collections;

public class Wander : Face
{
    //Wander circle radius and forward offset
    public float circleOffset=6f, circleRadius=3f;
    public float rate=2f; //Rate at which rotation can change
    float wanderOrientation, wanderTargetRotation; //target rotation
    Vector3 wanderTargetVect;

    Vector3 circleCenter, displacement;
    float wanderAngle;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        wanderOrientation = 0f;
        wanderAngle = 0f;
        circleCenter = character.transform.position.normalized * circleOffset;
    }

    // Update is called once per frame
    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    public override Steering GetSteering()
    {
        /*//circleCenter = character.velocity.normalized * circleOffset;

        circleCenter = character.transform.position.normalized * circleOffset;

        Debug.DrawRay(character.transform.position, circleCenter, Color.red);

        displacement = Vector3.up*circleRadius;

        //Debug.DrawRay(circleCenter + character.transform.position, displacement, Color.green);

        //SetAngle(displacement,wanderAngle);
        displacement = Quaternion.AngleAxis(wanderAngle, Vector3.forward) * (character.transform.position.normalized*circleRadius);

        Debug.DrawRay(circleCenter + character.transform.position, displacement, Color.green);

        wanderAngle = (Random.Range(-180f,180f)*rate - rate*0.5f);
        print(wanderAngle);

        Vector3 wanderForce = circleCenter + displacement;
        
        steering.linear = wanderForce;
        */
        
         //Update wander orientation
        wanderOrientation += Random.Range(-1f,1f) * rate;

        //Calculate combined target rotation
        //wanderTargetRotation = wanderOrientation + character.orientation;
        wanderTargetRotation = wanderOrientation + character.transform.rotation.eulerAngles.z;
        //wanderTargetRotation = wanderOrientation + character.rotation;
        //print(wanderTargetRotation);

        //Calculate center of wander circle
        //wanderTargetVect = character.transform.position + offset * GetOrientationAsVector(character.orientation);
        wanderTargetVect = character.transform.position + circleOffset * GetOrientationAsVector(character.transform.rotation.eulerAngles.z);
        //wanderTargetVect = character.transform.position + offset * GetOrientationAsVector(character.rotation);

        //Calculate target location
        wanderTargetVect += circleRadius * GetOrientationAsVector(wanderTargetRotation);

        // Delegate to face
        steering = base.GetSteering(wanderTargetVect);

        //steering.linear = character.maxAcc * GetOrientationAsVector(character.orientation);
        steering.linear = character.maxAcc * GetOrientationAsVector(character.transform.rotation.eulerAngles.z);
        
        return steering;
    }

    public static void SetAngle(Vector3 vector, float value)
    {
        float r = vector.magnitude;
        vector.x = Mathf.Cos(value) * r;
        vector.y = Mathf.Sin(value) * r;
    }

}
