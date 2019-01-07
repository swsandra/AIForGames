using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoToSignalState : State {

	GraphPathFollowing pathFollowing;

	GameObject invocant;

	GraphMap graph;

	List<Transition> transitions;

	public GoToSignalState(GameObject inv, List<Transition> trans){
		//Store path following script from gameobject
		invocant = inv;
		transitions= trans;
		name="goto";
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		graph = GameObject.Find("Map Graph").GetComponent<GraphMap>();
		pathFollowing.astar_target=null; //Set to null just in case
	}

	public override void GetAction(){
		Debug.Log(pathFollowing.path.Count);
		if(invocant.GetComponent<Sensor>().targetNode!=-1){
			pathFollowing.ChangeEndNode(invocant.GetComponent<Sensor>().targetNode);
		}
		return;
	}
	
	public override List<Transition> GetTransitions(){
		return transitions;
	}

}