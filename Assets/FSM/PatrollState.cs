using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrollState : State{

	//Patroll region, vectors indicating limits within patroll (min and max)
	Vector3[] patrollRegion;

	List<Transition> transitions;

	GraphPathFollowing pathFollowing;

	GameObject invocant;

	public PatrollState(GameObject inv, List<Transition> trans){
		invocant=inv;
		transitions = trans;
		name="patroll";
		patrollRegion = new Vector3[2];
		//North
		if (invocant.name=="Monster_Disgust") {
			patrollRegion[0] = new Vector3(-84f, 0f, 0f);
			patrollRegion[1] = new Vector3(76f, 64f, 0f);
		}//South
		else if (invocant.name=="Monster_Anger") {
			patrollRegion[0] = new Vector3(-123f, -88f, 0f);
			patrollRegion[1] = new Vector3(127f, -18f, 0f);
		}//All regions
		else if (invocant.name=="Monster_Sadness"){
			patrollRegion[0] = new Vector3(-104f, -88f, 0f);
			patrollRegion[1] = new Vector3(127f, 65f, 0f);
		}//If it is the kitchen and north
		else if (invocant.name=="Monster_Fear"){
			//patrollRegion[0] = new Vector3(-64f, 13f, 0f); //Part of north
			//patrollRegion[1] = new Vector3(148f, 64f, 0f);
			patrollRegion[0] = new Vector3(-104f, -88f, 0f);
			patrollRegion[1] = new Vector3(127f, 65f, 0f);
		}
		//Store path following script from gameobject
		pathFollowing = invocant.GetComponent<GraphPathFollowing>();
		pathFollowing.astar_target=null; //Set to null just in case
	}

	public override void GetAction(){
		//If path is empty, change for random target
		if (pathFollowing.path.Count==0){
			//Generate numbers between min and max
			float x = Random.Range(patrollRegion[0].x,patrollRegion[1].x);
			float y = Random.Range(patrollRegion[0].y,patrollRegion[1].y);
			//Get node of result vector
			int newTargetNode = pathFollowing.graph.GetNearestNodeByCenter(new Vector3(x, y, 0f));
			//Change astar target
			//Debug.Log("end node "+newTargetNode);
			pathFollowing.ChangeEndNode(newTargetNode);
		}
	}

	public override List<Transition> GetTransitions(){
		return transitions;
	}

}