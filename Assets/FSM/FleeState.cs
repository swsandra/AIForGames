using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FleeState : State{

	//Patroll region, vectors indicating limits within patroll (min and max)
	Vector3[] patrollRegion;

	List<Transition> transitions;

	GraphPathFollowing pathFollowing;

	GameObject invocant;

    float speed;

	public FleeState(GameObject inv, List<Transition> trans, float fleeSpeed){
		invocant=inv;
		transitions = trans;
		name="patroll";
		patrollRegion = new Vector3[2];
        patrollRegion[0] = new Vector3(96f, -3f, 0f);
        patrollRegion[1] = new Vector3(148f, 64f, 0f);
		//Store path following script from gameobject
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
        speed=fleeSpeed;
	}

	public override void GetAction(){
        invocant.GetComponent<Agent>().maxSpeed = speed;
        invocant.GetComponent<Agent>().maxAcc = (speed*2)+10;
		//If path is empty, change for random target
		if (pathFollowing.path.Count==0){
			//Generate numbers between min and max
			float x = Random.Range(patrollRegion[0].x,patrollRegion[1].x);
			float y = Random.Range(patrollRegion[0].y,patrollRegion[1].y);
			//Get node of result vector
			int newTargetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(x, y, 0f));
			//Change astar target
			pathFollowing.ChangeEndNode(newTargetNode);
		}
	}

	public override List<Transition> GetTransitions(){
		return transitions;
	}

}