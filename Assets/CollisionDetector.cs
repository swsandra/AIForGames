using UnityEngine;
using System.Collections;
using UnityEditor;

public class CollisionDetector : GeneralBehaviour
{

    public Vector3 centralRay, sideRay1, sideRay2;
    public Vector3 centralRayEnd, sideRay1End, sideRay2End;
    float angleVariation = 40f;
    
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

        centralRayEnd = centralRay + character.transform.position;
        sideRay1End = sideRay1 + character.transform.position;
        sideRay2End = sideRay2 + character.transform.position;

    }

    public bool LineIntersectsCircle(Vector3 rayStart, Vector3 rayEnd, Vector3 center, float radius)
    {
        //DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd);
        return HandleUtility.DistancePointLine(center, rayStart, rayEnd)<=radius;
    }

    public Vector3 RayIntersects(Vector3 center, float radius)
    {
        //Calculates which ray is intersecting some obstacle
        if (LineIntersectsCircle(centralRay, centralRayEnd, center, radius))
        {
            //print("c");
            return centralRayEnd;
        }

        if (LineIntersectsCircle(sideRay1, sideRay1End, center, radius))
        {
            //print("1");
            return sideRay1End;
        }

        if (LineIntersectsCircle(sideRay2, sideRay2End, center, radius))
        {
            //print("2");
            return sideRay2End;
        }

        return Vector3.positiveInfinity;
    }

    //Returns first collision
    public static GameObject GetCollision()
    {
        GameObject mostThreatening = null;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Obstacle");
                
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
