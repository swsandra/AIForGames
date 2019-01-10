using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Disgust_SM : MonoBehaviour{

	List<State> states;

	public State currentState;

	State initialState;

	Transition triggeredTransition;

	float initialSpeed;

	 // Use this for initialization
	void Start()
	{
		states = new List<State>();
		//Patroll state
		List<Transition> patrolltrans = new List<Transition>();
		patrolltrans.Add(new SeeMonsterTrans(gameObject));
		PatrollState patroll = new PatrollState(gameObject, patrolltrans);
		states.Add(patroll);
		
		//Scream state
        List<Transition> screamtrans = new List<Transition>();
		screamtrans.Add(new SeeMonsterTrans(gameObject));
        screamtrans.Add(new SeeAngerTrans(gameObject));
		screamtrans.Add(new TimePassTrans(gameObject,25f,"sleep"));
		ScreamState scream = new ScreamState(gameObject, screamtrans,10f);
		states.Add(scream);

		//Sleep state
        List<Transition> sleeptrans = new List<Transition>();
		sleeptrans.Add(new TimePassTrans(gameObject,40f,"patroll"));
		SleepState sleep = new SleepState(gameObject, sleeptrans);
		states.Add(sleep);

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
			//Debug.Log("Next state: "+targetState);
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