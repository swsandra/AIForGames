using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sadness_SM : MonoBehaviour{

	List<State> states;

	State initialState, currentState;

	Transition triggeredTransition;

	 // Use this for initialization
	void Start()
	{
		states = new List<State>();
		PatrollState patroll = new PatrollState(gameObject);
		states.Add(patroll);
		PursueState pursue = new PursueState(gameObject, 9);
		states.Add(pursue);
		initialState = patroll;
		currentState = patroll;
		triggeredTransition = null;
		gameObject.GetComponent<GraphPathFollowing>().astar_target=null; //Set to null just in case
		
		//THIS IS FOR TEST
		currentState = pursue;
		triggeredTransition = new SeeMonsterTrans(gameObject);
	}

	// Update is called once per frame
	void Update()
	{
		//Book algorithm
		/* triggeredTransition = null;
		foreach (Transition transition in currentState.GetTransitions()){
			if (transition.IsTriggered()){
				triggeredTransition = transition;
				break;
			}   
		}

		if (triggeredTransition!=null){
			currentState = triggeredTransition.GetTargetState();
		} 

		currentState.GetAction();
		*/



	}


}