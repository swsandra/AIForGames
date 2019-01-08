using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoToSignalState : State {

	GraphPathFollowing pathFollowing;

	GameObject invocant;

	GraphMap graph;

	List<Transition> transitions;

	float speed;

	public GoToSignalState(GameObject inv, List<Transition> trans, float pursueSpeed){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="goto";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		graph = GameObject.Find("Map Graph").GetComponent<GraphMap>();
		pathFollowing.astar_target=null; //Set to null just in case
		speed = pursueSpeed;
	}

	public override void GetAction(){
		//Debug.Log(pathFollowing.path.Count);
		invocant.GetComponent<Agent>().maxSpeed = speed;
        invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
		if(invocant.GetComponent<Sensor>().targetNode!=-1){
			pathFollowing.ChangeEndNode(invocant.GetComponent<Sensor>().targetNode);
		}
		return;
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}