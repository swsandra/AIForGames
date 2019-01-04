using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sensor : MonoBehaviour{

	Agent character;

    List<string> modalities;

    public float threshold;

    public bool newSignal;

    int targetNode;

    Vector3 target;

    GraphPathFollowing pathFollowing;

	 // Use this for initialization
	void Start()
	{
		character = gameObject.GetComponent<Agent>();
        modalities = new List<string>();
        if (gameObject.name=="Monster_Disgust") {
            //modalities.Add("hearing");
            //threshold = 4f;
        }
        else if (gameObject.name=="Monster_Anger") {
            modalities.Add("hearing");//When disgust talks
            threshold = 4f;
        }
        else if (gameObject.name=="Monster_Sadness"){
            //Test
            //modalities.Add("hearing");
            //
            modalities.Add("smell");
            threshold = 4f;
        }
        
        pathFollowing=gameObject.GetComponent<GraphPathFollowing>();
        pathFollowing.astar_target=null;
        newSignal=false;

	}

	// Update is called once per frame
	void Update()
	{
        //Notification of signals
        if(newSignal){
            targetNode = pathFollowing.graph.GetNearestNodeByCenter(target);
            //Change astar target
            pathFollowing.ChangeEndNode(targetNode);
            newSignal=false;
        }
	}

    //FUNCTION NEEDS TO BE CHANGED (?)
    public bool detectsModality(Modality mod){
        if (modalities.Contains(mod.name)){
            return true;
        }
        return false;
    }

    public void Notify(Signal signal){
        Debug.Log("Notifying of new signal.");
        newSignal=true;
        target=signal.position;
    }

}