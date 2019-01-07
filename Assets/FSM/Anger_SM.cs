using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Anger_SM : MonoBehaviour{

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
		patrolltrans.Add(new HearNoiseTrans(gameObject));
		PatrollState patroll = new PatrollState(gameObject, patrolltrans);
		states.Add(patroll);
		
        //Search state
		List<Transition> searchtrans = new List<Transition>();
		//searchtrans.Add();
		SearchState search = new SearchState(gameObject, searchtrans,10f);//Runs super fast
		states.Add(search);

		//Search Disgust state


        //Pursue state


        //Look for state


        //Push Fear state


        //Nearest waypoint state




		initialState = patroll;
		currentState = patroll;
        //TEST
        currentState = search;
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

        //TEST
        foreach (Transition transition in currentState.GetTransitions()){
			if (transition.IsTriggered()){
                Debug.Log("Transition triggered");
				triggeredTransition = transition;
				break;
			}   
		}

        currentState.GetAction();
        //

	}

}