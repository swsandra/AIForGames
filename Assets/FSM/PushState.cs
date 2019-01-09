using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PushState : State {

	GraphPathFollowing pathFollowing;

	GameObject invocant, message;
	
	Agent fear;

	List<Transition> transitions;

	DateTime creationTime;

	Separation separation;

	bool changeCreationTime;

	float initialSpeed, pushTime;

	public PushState(GameObject inv, List<Transition> trans){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="push";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
		changeCreationTime=true;
		fear = GameObject.Find("Monster_Fear").GetComponent<Agent>();
		separation = GameObject.Find("Monster_Fear").GetComponent<Separation>();
		initialSpeed = fear.maxSpeed;
		message=GameObject.Find("Evil Laugh");
		pushTime=4f;
	}

	public override void GetAction(){
		//Enable
		fear.SetSteering(separation.GetSteering(), separation.weight, separation.priority);
		separation.pushed=true;
		fear.maxSpeed=initialSpeed*2;
		//Enable text mesh renderer
		message.transform.position=new Vector3(invocant.transform.position.x,invocant.transform.position.y+15f,invocant.transform.position.z);
		message.GetComponent<Renderer>().enabled=true;

		//Activate separation for 2 seconds
		if (changeCreationTime){
			creationTime = System.DateTime.Now;
			changeCreationTime=false;
		}
		DateTime currentTime = System.DateTime.Now;
		if (Mathf.Abs((float)((currentTime - creationTime).TotalSeconds))>pushTime){
			//Disable
			fear.maxSpeed=initialSpeed;
			message.GetComponent<Renderer>().enabled=false;
			changeCreationTime=true;
			return;
		}
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}