using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sight : MonoBehaviour{

	float raySize = 30f, angleVariation = 45f;
    Vector3 sideRay1, sideRay2, centralRay;
    Vector3 sideRay1End, sideRay2End;

    Agent character;

    GameObject characters;

    List<string> inSightCharacters;

	 // Use this for initialization
	void Start()
	{
        //Generate raysfor triangle
		//Define points
        Vector3 sidePoint = (gameObject.transform.position) + Vector3.up * raySize;

        //Define rays to make triangle
        Vector3 sideRay = sidePoint - gameObject.transform.position;
        sideRay1 = Quaternion.AngleAxis(-angleVariation, Vector3.forward) * sideRay;
        sideRay2 = Quaternion.AngleAxis(angleVariation, Vector3.forward) * sideRay;

        //Define base vector for ray rotation
        Vector3 centralPoint = (gameObject.transform.position) + Vector3.up;
        centralRay = centralPoint - gameObject.transform.position;

        character = gameObject.GetComponent<Agent>();
        characters = GameObject.Find("Characters");

        //inSightCharacters = new List<string>();
	}

	// Update is called once per frame
	void Update()
	{
        GenerateSightTriangle();
		inSightCharacters = CheckSightTriangle(2f);
        if (inSightCharacters.Count!=0){
            Debug.Log("Characters in sight: ");
            foreach (string name in inSightCharacters){
                Debug.Log(" "+name);
            }
        }
	}

    public void GenerateSightTriangle()
    {
        //Rotate rays in direction of character movement
        //First rotate the central ray
        centralRay = Vector3.RotateTowards(centralRay, character.velocity.normalized * centralRay.magnitude, character.maxSpeed * Time.deltaTime, 0f);

        //Rotate siderays 
        sideRay1 = Vector3.RotateTowards(sideRay1, character.velocity.normalized * sideRay1.magnitude, character.maxSpeed * Time.deltaTime, 0f);
        sideRay2 = Vector3.RotateTowards(sideRay2, character.velocity.normalized * sideRay2.magnitude, character.maxSpeed * Time.deltaTime, 0f);
        
        sideRay1 = Quaternion.AngleAxis(-angleVariation,Vector3.forward)*(centralRay*raySize); //If * anysideray looks like a 
        sideRay2 = Quaternion.AngleAxis(angleVariation, Vector3.forward)*(centralRay*raySize);

        sideRay1End = sideRay1 + character.transform.position;
        sideRay2End = sideRay2 + character.transform.position;

        Debug.DrawRay(gameObject.transform.position, sideRay1, Color.green);
        Debug.DrawRay(gameObject.transform.position, sideRay2, Color.green);
        Debug.DrawLine(sideRay1End, sideRay2End, Color.red);
    }

    public List<string> CheckSightTriangle(float delta){
        
        List<string> collidingCharacters = new List<string>();
        
        foreach (Transform child in characters.transform){
            if (child.gameObject.name.Equals(gameObject.name)){
                continue;
            }
            Vector3 position = child.transform.position;
            // A is transform position
            // B is sideray1end and C is sideray2end
            Vector3 A = gameObject.transform.position;
            Vector3 B = sideRay1End;
            Vector3 C = sideRay2End;
            
            //Calculate areas
            float ABC = (1f/2f)* Mathf.Abs(((A.x-C.x)*(B.y-A.y))-((A.x-B.x)*(C.y-A.y)));
            float ABP = (1f/2f)* Mathf.Abs(((A.x-position.x)*(B.y-A.y))-((A.x-B.x)*(position.y-A.y)));
            float BCP = (1f/2f)* Mathf.Abs(((B.x-position.x)*(C.y-B.y))-((B.x-C.x)*(position.y-B.y)));
            float ACP = (1f/2f)* Mathf.Abs(((A.x-position.x)*(C.y-A.y))-((A.x-C.x)*(position.y-A.y)));
            float sumOfAreas = ABP + BCP + ACP;

            if (sumOfAreas <= delta || sumOfAreas <= ABC){
                collidingCharacters.Add(child.name);
            }
        }
        
        return collidingCharacters;
    }

}