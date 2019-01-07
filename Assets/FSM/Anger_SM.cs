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
        patrolltrans.Add(new GetSignalTrans(gameObject));
		patrolltrans.Add(new HearNoiseTrans(gameObject));
		PatrollState patroll = new PatrollState(gameObject, patrolltrans);
		states.Add(patroll);
		
        //Search noise state
		List<Transition> searchnoisetrans = new List<Transition>();
        searchnoisetrans.Add(new GetSignalTrans(gameObject));
		searchnoisetrans.Add(new TimePassTrans(gameObject,7f,"patroll"));
		SearchNoiseState search = new SearchNoiseState(gameObject, searchnoisetrans,10f);//Runs super fast
		states.Add(search);

		//Search Disgust (go to signal state)
        List<Transition> searchdisgusttrans = new List<Transition>();
		searchdisgusttrans.Add(new StopAndTimePassTrans(gameObject,3f,"searchlocation"));
		GoToSignalState gotosignal = new GoToSignalState(gameObject, searchdisgusttrans);
		states.Add(gotosignal);

        //Search location state
        List<Transition> searchlocationtrans = new List<Transition>();
        searchlocationtrans.Add(new GetSignalTrans(gameObject));
		searchlocationtrans.Add(new TimePassTrans(gameObject,2f,"patroll")); //No hay nadie alrededor del punto
		SearchLocationState searchLocation = new SearchLocationState(gameObject, searchlocationtrans,8f);//Runs super fast
		states.Add(searchLocation);

        //Pursue state


        //Look for state


        //Push Fear state


        //Nearest waypoint state




		initialState = patroll;
		currentState = patroll;
        //TEST
        currentState = gotosignal;
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

        //currentState.GetAction();
        //

	}

}