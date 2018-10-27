using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : DSeek
{
    //How far to avoid collision
    float avoidDistance=4f;
    //Length of the collision ray
    float lookAhead=10f;
    //Collision ray vector
    Vector3 targetPosition;
    public GameObject[] targets;
    CollisionDetector collisionDetector = new CollisionDetector();

    // Use this for initialization
    new void Start()
    {
        base.Start();
        collisionDetector.GenerateRays(character, target);
    }

    // Update is called once per frame
    void Update()
    {
        character.steering.linear = GetSteering().linear;
    }

    private GameObject FindMostThreateningObstacle()
    {
        GameObject mostThreatening = null;
        
        //print(objects[0]);

        foreach (GameObject target in targets)
        {
            //CAMBIAR POR VER SI INTERSECTA CON ALGUN RAYO
            //bool collision = LineIntersectsCircle(rayVector, rayVector2, obj.GetComponent<CircleCollider2D>());


            //if (collision && (mostThreatening==null || Vector3.Distance(transform.position,target.transform.position) < Vector3.Distance(transform.position, mostThreatening.transform.position)) )
            //{
            //    mostThreatening = target;
            //}
        }

        return mostThreatening;
    }

    public override Steering GetSteering()
    {
        //Calculate the collision ray vector
        collisionDetector.RotateRays(character);

        Debug.DrawRay(character.transform.position, collisionDetector.centralRay, Color.red); //rays[0] + character.transform.position es el final del rayo
        //Debug.DrawLine(character.transform.position, collisionDetector.centralRay + character.transform.position, Color.red);
        Debug.DrawRay(character.transform.position, collisionDetector.sideRay1, Color.green);
        Debug.DrawRay(character.transform.position, collisionDetector.sideRay2, Color.blue);

        

        //foreach (Vector3 ray in rays)
        //{
            //print(ray);
        //}

        GameObject mostThreatening = FindMostThreateningObstacle();
        Vector3 avoidance = Vector3.zero;
        //print(mostThreatening);
        if (mostThreatening != null)
        {
            //avoidance.x = rayVector.x - mostThreatening.GetComponent<CircleCollider2D>().bounds.center.x;
            //avoidance.y = rayVector.y - mostThreatening.GetComponent<CircleCollider2D>().bounds.center.y;
            avoidance.Normalize();
            avoidance *= character.maxAcc;
        }
        else
        {
            avoidance *= 0f;
        }

        steering.linear = avoidance;

        //return steering;

        //RaycastHit hit; //Acts as collision structure in book algorithm
        //Debug.DrawRay(character.transform.position, rayVector, Color.red,2,false);
        //Debug.DrawRay(character.transform.position, rayVector2, Color.green, 2, false);
        //Find collision

        //if (Physics.Raycast(character.transform.position, rayVector, out hit, lookAhead))
        //{
        //    targetPosition = hit.point + hit.normal * avoidDistance;
        //    print("target transform");
        //    print(target.transform.position);
        //    print("target calculated");
        //    print(targetPosition);
        //    return base.GetSteering(targetPosition);
        //}

        return base.GetSteering(target.transform.position);
        //return steering;
    }

    

}


