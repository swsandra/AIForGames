using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : DSeek
{
    //How far to avoid collision
    float avoidDistance=5f;
    //Length of the collision ray
    float lookAhead=10f;
    //Collision ray vector
    Vector3 rayVector, targetPosition;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        character.steering = GetSteering();
    }

    public override Steering GetSteering()
    {
        //Calculate target to delegate to seek
        //Calculate the collision ray vector
        rayVector = character.velocity.normalized * lookAhead;

        RaycastHit hit; //Acts as collision in book algorithm

        //Find collision
        if (Physics.Raycast(character.transform.position, rayVector, out hit, lookAhead))
        {
            targetPosition = hit.point + hit.normal * avoidDistance;
            
            return base.GetSteering(targetPosition);
        }

        return steering;
    }

}
