using UnityEngine;
using System.Collections;

public class Wander : Face
{
    //Wander circle radius and forward offset
    public float offset=12f, radius=5f;
    public float rate=2f; //Rate at which rotation can change
    float wanderOrientation, wanderTargetRotation; //target rotation
    Vector3 wanderTargetVect;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        wanderOrientation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {
        //Update wander orientation
        wanderOrientation += Random.Range(-1f,1f) * rate;

        //Calculate combined target rotation
        //wanderTargetRotation = wanderOrientation + character.orientation;
        wanderTargetRotation = wanderOrientation + character.transform.rotation.eulerAngles.z;
        //wanderTargetRotation = wanderOrientation + character.rotation;
        //print(wanderTargetRotation);

        //Calculate center of wander circle
        //wanderTargetVect = character.transform.position + offset * GetOrientationAsVector(character.orientation);
        wanderTargetVect = character.transform.position + offset * GetOrientationAsVector(character.transform.rotation.eulerAngles.z);
        //wanderTargetVect = character.transform.position + offset * GetOrientationAsVector(character.rotation);

        //Calculate target location
        wanderTargetVect += radius * GetOrientationAsVector(wanderTargetRotation);

        // Delegate to face
        steering = base.GetSteering(wanderTargetVect);

        steering.linear = character.maxAcc * GetOrientationAsVector(character.orientation);
        steering.linear = character.maxAcc * GetOrientationAsVector(character.transform.rotation.eulerAngles.z);
        
        return steering;
    }

}
