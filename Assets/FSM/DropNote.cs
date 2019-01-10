using UnityEngine;
using System;
using System.Collections;

public class DropNote : MonoBehaviour{

	GameObject note;

	Disgust_SM stateMachine;

	DateTime creationTime;

	bool changeCreationTime;

	// Use this for initialization
	void Start()
	{
		note = GameObject.Find("Note");
		note.GetComponent<Renderer>().enabled=false;
		stateMachine = GameObject.Find("Monster_Disgust").GetComponent<Disgust_SM>();
		changeCreationTime=true;
	}

	// Update is called once per frame
	void Update()
	{
		if(stateMachine.currentState.name=="sleep"){
			if (changeCreationTime){
				creationTime = System.DateTime.Now;
				changeCreationTime=false;
			}
			DateTime currentTime = System.DateTime.Now;
			if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>15f){
				note.GetComponent<Renderer>().enabled=false;
			}else{
				note.GetComponent<Renderer>().enabled=true;
			}
		}else{
			note.GetComponent<Renderer>().enabled=false;
			changeCreationTime=true;
		}
	}

}