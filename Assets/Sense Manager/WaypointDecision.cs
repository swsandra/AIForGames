using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointDecision : MonoBehaviour{

	//Nodes of waypoints
	Vector3[] waypoints;

	public bool calcWaypoint;

	//GraphPathFollowing pathFollowing;

	GraphMap graph;

	GameObject pursuer;    

	 // Use this for initialization
	void Start()
	{
		//Store waypoints
		waypoints = new Vector3[3];
		GameObject map = GameObject.Find("Map Graph");
		graph = map.GetComponent<GraphMap>();
		waypoints[0]=new Vector3(-116f,62f,0f);
		waypoints[1]=new Vector3(-3f,-30f,0f);
		waypoints[2]=new Vector3(141f,-90f,0f);
		

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
		foreach(Vector3 waypoint in waypoints){
			Debug.Log("Now testing node "+graph.GetNearestNodeByCenter(waypoint));
			if(nextWaypoints.Count==0){
				nextWaypoints.Add(graph.GetNearestNodeByCenter(waypoint));
			}else{//Iterate until greater distance found
				//Calculate distance from waypoint to character
				float distance = (waypoint-transform.position).magnitude;
				int j=0;
				foreach(int nextWaypoint in nextWaypoints){
					float nextDistance =(graph.nodes[nextWaypoint].center-transform.position).magnitude;
					Debug.Log("Distance "+ distance +" nextdistance "+ nextDistance);
					if(nextDistance>distance){
						nextWaypoints.Insert(j,graph.GetNearestNodeByCenter(waypoint));
						break;
					}else if (nextWaypoints.Count==1){
						nextWaypoints.Add(graph.GetNearestNodeByCenter(waypoint));
					}
					j++;
				}
			}
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