using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SleepState : State {

	GraphPathFollowing pathFollowing;

    int targetNode;

	GameObject invocant;

	List<Transition> transitions;

	public SleepState(GameObject inv, List<Transition> trans){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="sleep";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
        //targetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(-115.9f,-40.3f,0f));
	}

	public override void GetAction(){
		//Changes speed of character
        targetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(-115.9f,-40.3f,0f));
        pathFollowing.ChangeEndNode(targetNode);		
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}