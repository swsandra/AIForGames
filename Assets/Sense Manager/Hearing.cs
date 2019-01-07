using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hearing : MonoBehaviour{


    GameObject characters;

    public List<string> heardCharacters, heardCloseCharacters;

    public float maxRadius, minRadius;

	 // Use this for initialization
	void Start()
	{
        characters = GameObject.Find("Characters");
        minRadius=20f;
        maxRadius=35f;
	}

	// Update is called once per frame
	void Update()
	{
        DrawHearingArea();
        heardCloseCharacters = CheckMinArea();
        heardCharacters = CheckMinArea();
        
	}

    public void DrawHearingArea()
    {
        DebugExtension.DebugCircle(gameObject.transform.position,Vector3.forward,Color.magenta,maxRadius);
        DebugExtension.DebugCircle(gameObject.transform.position,Vector3.forward,Color.magenta,minRadius);
    }

    public List<string> CheckMinArea(){
        
        List<string> collidingCharacters = new List<string>();
        
        foreach (Transform child in characters.transform){
            if (child.gameObject.name.Equals(gameObject.name)){
                continue;
            }
            //
            Vector3 distance = child.transform.position - transform.position;
            if (distance.magnitude<minRadius){
                collidingCharacters.Add(child.name);
            }
        }

        return collidingCharacters;
    }

    public List<string> CheckMaxArea(){
        
        List<string> collidingCharacters = new List<string>();
        
        foreach (Transform child in characters.transform){
            if (child.gameObject.name.Equals(gameObject.name)){
                continue;
            }
            //
            Vector3 distance = child.transform.position - transform.position;
            if (distance.magnitude<maxRadius){
                collidingCharacters.Add(child.name);
            }
        }

        return collidingCharacters;
    }

}