using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointDecision : MonoBehaviour{

	//Nodes of waypoints
	int[] waypoints;

	public bool calcWaypoint;

	//GraphPathFollowing pathFollowing;

	GraphMap graph;

	GameObject pursuer;    

	 // Use this for initialization
	void Start()
	{
		//Store waypoints
		waypoints = new int[3];
		GameObject map = GameObject.Find("Map Graph");
		graph = map.GetComponent<GraphMap>();
		waypoints[0]=graph.GetNearestNodeByCenter(new Vector3(-116f,62f,0f));
		waypoints[1]=graph.GetNearestNodeByCenter(new Vector3(-3f,-30f,0f));
		waypoints[2]=graph.GetNearestNodeByCenter(new Vector3(141f,-90f,0f));

		Debug.Log("Waypoints are at nodes "+waypoints[0]+" "+waypoints[1]+" "+waypoints[2]);

		//pathFollowing=gameObject.GetComponent<GraphPathFollowing>();
		//pathFollowing.astar_target=null;

		calcWaypoint=false;

	}

	// Update is called once per frame
	void Update()
	{
		if (calcWaypoint){
			List<int> nearestWaypoints = GetNearestWaypoints();
			if (nearestWaypoints.Count!=0){
				foreach(int waypoint in nearestWaypoints){
					Debug.Log("Waypoint at "+ graph.nodes[waypoint].center +" node "+waypoint);
				}
			}
			calcWaypoint=false;
		}
	}

	public List<int> GetNearestWaypoints(){
		List<int> nextWaypoints = new List<int>();
		Dictionary<int,float> waypointDistance = new Dictionary<int,float>();
		for(int i=0;i<waypoints.Length;i++){
			//Calculate distances for each point
			waypointDistance[waypoints[i]] = (graph.nodes[waypoints[i]].center-transform.position).magnitude;
		}
		//Reorder from nearest
		while (waypointDistance.Count>0){
			//Calculate smaller distance
			float nearestDistance=Mathf.Infinity;
			int nearest=-1;
			foreach(KeyValuePair<int, float> waypoint in waypointDistance){
				if(waypoint.Value<nearestDistance){
					nearestDistance=waypoint.Value;
					nearest=waypoint.Key;
				}
			}
			//Add waypoint to nearest
			nextWaypoints.Add(nearest);
			//Remove from dictionary
			waypointDistance.Remove(nearest);
		}
		return nextWaypoints;
	}

	public int BestWaypoint(){
		int bestWaypoint = -1;
		List<int> nearestWaypoints = GetNearestWaypoints();
		//Calculare if waypoints are in pursuers line of sight


		return bestWaypoint;
	}

}