using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sadness_SM : MonoBehaviour{

	List<State> states;

	State initialState, currentState;

	Transition triggeredTransition;

	float initialSpeed;

	 // Use this for initialization
	void Start()
	{
		states = new List<State>();
		//Transitions for patroll state
		List<Transition> patrolltrans = new List<Transition>();
		patrolltrans.Add(new SeeMonsterTrans(gameObject));
		PatrollState patroll = new PatrollState(gameObject, patrolltrans);
		states.Add(patroll);
		//Transitions for pursue state
		List<Transition> pursuetrans = new List<Transition>();
		pursuetrans.Add(new StopSeeMonsterTrans(gameObject));
		PursueState pursue = new PursueState(gameObject, pursuetrans, 9f);
		states.Add(pursue);
		//Transitions for look for state
		List<Transition> looktrans = new List<Transition>();
		looktrans.Add(new SeeMonsterTrans(gameObject));
		looktrans.Add(new TimePassTrans(gameObject,5f));
		LookForState look = new LookForState(gameObject,looktrans,7f);
		states.Add(look);
		//Transitions for eat state
		List<Transition> eattrans = new List<Transition>();
		eattrans.Add(new GetSignalTrans(gameObject)); //INCORRECT
		EatState eat = new EatState(gameObject,eattrans);
		states.Add(eat);

		initialState = patroll;
		currentState = patroll;
		//TEST
		currentState = eat;
		//
		triggeredTransition = null;
		gameObject.GetComponent<GraphPathFollowing>().astar_target=null; //Set to null just in case
		initialSpeed = gameObject.GetComponent<Agent>().maxSpeed;
	}

	// Update is called once per frame
	void Update()
	{
		//Book algorithm
		/*triggeredTransition = null;
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
				gameObject.GetComponent<Agent>().maxSpeed = initialSpeed;
        		gameObject.GetComponent<Agent>().maxAcc = (initialSpeed*2)+10;
				if(targetState.Equals(state.name)){
					currentState = state;
				}
			}
		}

		currentState.GetAction(); */
		foreach (Transition transition in currentState.GetTransitions()){
			if (transition.IsTriggered()){
				Debug.Log("Transition triggered");
				triggeredTransition = transition;
				break;
			}   
		}
		
	}

}