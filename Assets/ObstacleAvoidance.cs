using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : GeneralBehaviour
{
    //How far to avoid collision
    float avoidDistance=2f;
    //Length of the collision ray
    float lookAhead=10f;
    //Collision ray vector
    Vector3 targetPosition, threatenedRay;
    //public GameObject[] targets;
    CollisionDetector collisionDetector = new CollisionDetector();
    GameObject[] targets;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        collisionDetector.GenerateRays(character, target);
        targets = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    // Update is called once per frame
    void Update()
    {
        character.steering.linear = GetSteering().linear;
    }

    private GameObject FindMostThreateningObstacle()
    {
        GameObject mostThreatening = null;

        foreach (GameObject target in targets)
        {
            Vector3 center = target.GetComponent<Transform>().position;
            Vector3 intersectingRay = collisionDetector.RayIntersects(center,avoidDistance);
            bool collision = intersectingRay.magnitude < Vector3.positiveInfinity.magnitude;

            if (collision && (mostThreatening==null || Vector3.Distance(character.transform.position, center) < Vector3.Distance(character.transform.position, mostThreatening.transform.position)) )
            {
                mostThreatening = target;
                threatenedRay = intersectingRay;
            }
        }

        return mostThreatening;
    }

    public override Steering GetSteering()
    {
        //Calculate the collision ray vector
        collisionDetector.RotateRays(character);

        Debug.DrawRay(character.transform.position, collisionDetector.centralRay, Color.red); //rays[0] + character.transform.position es el final del rayo
        Debug.DrawRay(character.transform.position, collisionDetector.sideRay1, Color.green);
        Debug.DrawRay(character.transform.position, collisionDetector.sideRay2, Color.blue);
        //Debug.DrawLine(character.transform.position, collisionDetector.centralRayEnd, Color.red);

        //collisionDetector.RayIntersects(center,avoidDistance);

        GameObject mostThreatening = FindMostThreateningObstacle();
        Vector3 avoidance = Vector3.zero;
        //print(mostThreatening);
        if (mostThreatening != null)
        {
            avoidance.x = threatenedRay.x - mostThreatening.GetComponent<Transform>().position.x;
            avoidance.y = threatenedRay.y - mostThreatening.GetComponent<Transform>().position.y;
            avoidance.Normalize();
            avoidance *= character.maxAcc;
        }
        else
        {
            avoidance *= 0f;
        }

        steering.linear = avoidance;

        return steering;
        
    }
}