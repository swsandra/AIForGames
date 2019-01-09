using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PursuerWaypointState : State {

	GraphPathFollowing pathFollowing;

	float speed;

	GameObject invocant;

	WaypointDecision waypoint;

	List<Transition> transitions;

	public PursuerWaypointState(GameObject inv, List<Transition> trans, float pursueSpeed){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="pursuerwaypoint";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
		speed = pursueSpeed;
		waypoint=GameObject.Find("Waypoint").GetComponent<WaypointDecision>();
	}

	public override void GetAction(){
		//Changes speed of character
		invocant.GetComponent<Agent>().maxSpeed = speed;
		invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;

        //Calculate best pursuer waypoint
		if(pathFollowing.path.Count==0){
			int target = waypoint.BestPursuerWaypoint(invocant.transform.position);
			pathFollowing.ChangeEndNode(target);
		}		
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}