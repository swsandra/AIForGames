using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SearchLocationState : State {

	GraphPathFollowing pathFollowing;

	float speed;

	GameObject invocant;

	List<Transition> transitions;

	public SearchLocationState(GameObject inv, List<Transition> trans, float pursueSpeed){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="searchlocation";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
		speed = pursueSpeed;
	}

	public override void GetAction(){
		//Changes speed of character
		invocant.GetComponent<Agent>().maxSpeed = speed;
		invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
		
		Vector3 target = GameObject.Find("Monster_Disgust").GetComponent<Talk>().target;
		int targetNode = pathFollowing.graph.GetNearestNodeByCenter(target);
		pathFollowing.ChangeEndNode(targetNode);
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}