using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class WaypointDecision : MonoBehaviour{

	//Nodes of waypoints
	int[] waypoints;

	public bool calcWaypoint;

	//GraphPathFollowing pathFollowing;

	GraphMap graph;

	GameObject pursuer;  

	//Radius between wall or obstacle and waypoint
	float checkRadius=35f;

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

		Debug.Log("This is test");
		pursuer=GameObject.Find("Monster_Happiness");
		//pursuer=GameObject.Find("Monster_Anger");

		calcWaypoint=false;

	}

	// Update is called once per frame
	void Update()
	{
		int waypoint;
		if (calcWaypoint){
			waypoint = BestWaypoint();
			Debug.Log("Best waypoint at node "+waypoint);
			calcWaypoint=false;
		}
		/*GameObject walls = GameObject.Find("Walls");
		foreach(Transform wall in walls.transform){
			SpriteRenderer sprite;
			sprite=wall.GetComponent<SpriteRenderer>();
			Vector3 spriteSize = (sprite.bounds.size*0.5f);
			Debug.DrawLine(wall.position,wall.position+spriteSize,Color.magenta);

		} */
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
	
		//Calculate if waypoints are in pursuers line of sight
		foreach(int waypoint in nearestWaypoints){
			Debug.Log("Working waypoint at node "+waypoint);
			bool takeWaypoint = false;
			//First find all walls or obstacles within a certain radius
			GameObject walls = GameObject.Find("Walls");
			List<Transform> nearObjects = new List<Transform>();
			foreach(Transform wall in walls.transform){
				if (Vector3.Distance(graph.nodes[waypoint].center,wall.position)<=checkRadius){
					nearObjects.Add(wall);
				}
			}
			//Check obstacles too
			GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
			foreach(GameObject obstacle in obstacles){
				if (Vector3.Distance(graph.nodes[waypoint].center,obstacle.transform.position)<=checkRadius){
					nearObjects.Add(obstacle.transform);
				}
			}
			Debug.Log("There are "+nearObjects.Count+" near objects.");
			//Ray between pursuer and waypoint
			Vector3 lineStart = pursuer.transform.position;
			Vector3 lineEnd = graph.nodes[waypoint].center;
			foreach(Transform obj in nearObjects){
				SpriteRenderer sprite;
				sprite=obj.GetComponent<SpriteRenderer>();
				Vector3 spriteSizeVector = (sprite.bounds.size*0.5f);
				float spriteSize = Vector3.Distance(obj.position,obj.position+spriteSizeVector);
				if (HandleUtility.DistancePointLine(obj.position,lineStart,lineEnd)<=spriteSize){
					//Then this waypoint is not good or the object is not in sight line
					Debug.Log("Object "+obj.gameObject.name+" is in sight line.");
					takeWaypoint = true;
					break;
				}
			}

			if (takeWaypoint){
				bestWaypoint=waypoint;
				break;
			}

		}


		return bestWaypoint;
	}

}