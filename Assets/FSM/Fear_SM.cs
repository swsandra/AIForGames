using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fear_SM : MonoBehaviour{

	List<State> states;

	State initialState, currentState;

	Transition triggeredTransition;

	float initialSpeed;

	 // Use this for initialization
	void Start()
	{
		states = new List<State>();
		//Patroll state
		List<Transition> patrolltrans = new List<Transition>();
		patrolltrans.Add(new SeeMonsterTrans(gameObject));
		patrolltrans.Add(new PushedTrans(gameObject)); //is being pushed
		patrolltrans.Add(new TimePassTrans(gameObject,30f,"patrollonce"));
		PatrollState patroll = new PatrollState(gameObject, patrolltrans);
		states.Add(patroll);
		
		//Patroll once state
		List<Transition> patrolloncetrans = new List<Transition>();
		patrolloncetrans.Add(new SeeMonsterTrans(gameObject));
		patrolloncetrans.Add(new PushedTrans(gameObject)); //is being pushed
		patrolloncetrans.Add(new TimePassTrans(gameObject,15f,"patroll"));
		PatrollOnceState patrollonce = new PatrollOnceState(gameObject, patrolloncetrans);
		states.Add(patrollonce);

		//Flee state
		List<Transition> fleetrans = new List<Transition>();
		fleetrans.Add(new SeeMonsterTrans(gameObject));
		fleetrans.Add(new PushedTrans(gameObject)); //is being pushed
		fleetrans.Add(new TimePassTrans(gameObject,15f,"patroll"));
		FleeState flee = new FleeState(gameObject, fleetrans, 10f);
		states.Add(flee);

		//Waypoint decision state
		List<Transition> waypointtrans = new List<Transition>();
		waypointtrans.Add(new PushedTrans(gameObject));
		waypointtrans.Add(new StopAndTimePassTrans(gameObject,7f,"flee"));
		WaypointState waypoint = new WaypointState(gameObject, waypointtrans, 12f);
		states.Add(waypoint);

		//Dizzy state
		List<Transition> dizzytrans = new List<Transition>();
		dizzytrans.Add(new StopAndTimePassTrans(gameObject,1f,"waypoint"));
		DizzyState dizzy = new DizzyState(gameObject, dizzytrans, 3f);
		states.Add(dizzy);

		initialState = patroll;
		currentState = patroll;
		triggeredTransition = null;
		gameObject.GetComponent<GraphPathFollowing>().astar_target=null; //Set to null just in case
		initialSpeed = gameObject.GetComponent<Agent>().maxSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		//Book algorithm
		triggeredTransition = null;
		foreach (Transition transition in currentState.GetTransitions()){
			if (transition.IsTriggered()){
				triggeredTransition = transition;
				break;
			}   
		}

		if (triggeredTransition!=null){
			string targetState = triggeredTransition.GetTargetState();
			Debug.Log("Next state: "+targetState);
			//Get state from states list
			foreach (State state in states){
				if(targetState.Equals(state.name)){
					currentState = state;
					gameObject.GetComponent<Agent>().maxSpeed = initialSpeed;
					gameObject.GetComponent<Agent>().maxAcc = (initialSpeed*2)+10;
				}
			}
		}

		currentState.GetAction();

	}

}