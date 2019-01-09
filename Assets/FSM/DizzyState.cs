using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DizzyState : State {

	GraphPathFollowing pathFollowing;

	float speed;

	GameObject invocant;

	List<Transition> transitions;

	public DizzyState(GameObject inv, List<Transition> trans, float pursueSpeed){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="dizzy";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
		speed = pursueSpeed;
	}

	public override void GetAction(){
		//Changes speed of character
		invocant.GetComponent<Agent>().maxSpeed = speed;
		invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;

		int targetNode = pathFollowing.graph.GetNearestNodeByCenter(invocant.transform.position);
		pathFollowing.ChangeEndNode(targetNode);
		
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}