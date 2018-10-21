using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : DSeek
{
    //How far to avoid collision
    float avoidDistance=4f;
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
        //Vector3 position = character.transform.position;
        //Vector3 position = gameObject.transform.position;
        rayVector = character.velocity.normalized * lookAhead;
        //rayVector += position;
        //Vector3 direction = rayVector - position;
        //print(direction);
        RaycastHit hit; //Acts as collision structure in book algorithm
        Debug.DrawRay(character.transform.position,rayVector,Color.red,2,false);
        //Find collision
        if (Physics.Raycast(character.transform.position, rayVector, out hit, lookAhead))
        {
            targetPosition = hit.point + hit.normal * avoidDistance;
            print("target transform");
            print(target.transform.position);
            print("target calculated");
            print(targetPosition);
            return base.GetSteering(targetPosition);
        }

        return base.GetSteering(target.transform.position);
        //return steering;
    }

}
