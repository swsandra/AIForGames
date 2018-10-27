using UnityEngine;
using System.Collections;

public class CollisionDetector : GeneralBehaviour
{

    public Vector3 centralRay, sideRay1, sideRay2;
    public Vector3 centralRayEnd, sideRay1End, sideRay2End;
    float angleVariation = 40f;
    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateRays(Agent character, Agent target)
    {
        float lookAhead = 6f;
        float lookSide = lookAhead/2f;

        //Define points
        Vector3 centralPoint = (character.transform.position) + (Vector3.up * lookAhead);
        Vector3 sidePoint = (character.transform.position) + Vector3.up * lookSide;

        //Define rays
        Vector3 sideRay = sidePoint - character.transform.position;
        centralRay = centralPoint - character.transform.position;
        sideRay1 = Quaternion.AngleAxis(-angleVariation, Vector3.forward) * sideRay;
        sideRay2 = Quaternion.AngleAxis(angleVariation, Vector3.forward) * sideRay;

    }

    public void RotateRays(Agent character)
    {
        //Rotate rays in direction of character movement
        //First rotate the central ray
        centralRay = Vector3.RotateTowards(centralRay, character.velocity.normalized * centralRay.magnitude, character.maxSpeed * Time.deltaTime, 0f);

        //Rotate siderays 
        sideRay1 = Vector3.RotateTowards(sideRay1, character.velocity.normalized * sideRay1.magnitude, character.maxSpeed * Time.deltaTime, 0f);
        sideRay2 = Vector3.RotateTowards(sideRay2, character.velocity.normalized * sideRay2.magnitude, character.maxSpeed * Time.deltaTime, 0f);
        Vector3 halfCentralRay = centralRay.normalized*(centralRay.magnitude* 0.5f);
        sideRay1 = Quaternion.AngleAxis(-angleVariation,Vector3.forward) * halfCentralRay;
        sideRay2 = Quaternion.AngleAxis(angleVariation, Vector3.forward) * halfCentralRay;

    }

    public static bool LineIntersectsCircle(Vector3 rayStart, Vector3 rayEnd, Vector3 center, float radius)
    {
        return Vector3.Distance(center, rayStart) <= radius || Vector3.Distance(center, rayEnd) <= radius;
    }

    //Returns first collision
    public static GameObject GetCollision()
    {
        GameObject mostThreatening = null;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");

        print(objects[0]);

        foreach (GameObject obj in objects)
        {
            //bool collision = LineIntersectsCircle(rayVector, rayVector2, obj.GetComponent<CircleCollider2D>());
            //if (collision && (mostThreatening == null || Distance(transform.position, obj.transform.position) < Distance(transform.position, mostThreatening.transform.position)))
            //{
            //    mostThreatening = obj;
            //}
        }

        return mostThreatening;
    }

    
}
