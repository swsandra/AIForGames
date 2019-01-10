using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class WaypointDecision : MonoBehaviour{

	//Nodes of waypoints
	int[] waypoints, pursuerWaypoints;

	public bool calcWaypoint;
	GraphMap graph;

	GameObject pursuer;  

	//Radius between wall or obstacle and waypoint
	float checkRadius=35f;

	 // Use this for initialization
	void Start()
	{
		//Store waypoints
		waypoints = new int[3];
		pursuerWaypoints = new int[3];
		GameObject map = GameObject.Find("Map Graph");
		graph = map.GetComponent<GraphMap>();
		waypoints[0]=graph.GetNearestNodeByCenter(new Vector3(-116f,62f,0f));
		waypoints[1]=graph.GetNearestNodeByCenter(new Vector3(-3f,-7f,0f));
		waypoints[2]=graph.GetNearestNodeByCenter(new Vector3(141f,-90f,0f));

		pursuerWaypoints[0]=graph.GetNearestNodeByCenter(new Vector3(-114f,46f,0f));
		pursuerWaypoints[1]=graph.GetNearestNodeByCenter(new Vector3(5f,3f,0f));
		pursuerWaypoints[2]=graph.GetNearestNodeByCenter(new Vector3(137f,-77f,0f));

		pursuer=GameObject.Find("Monster_Anger");

		calcWaypoint=false;

	}

	// Update is called once per frame
	void Update()
	{
		/*int waypoint;
		if (calcWaypoint){
			waypoint = BestWaypoint();
			Debug.Log("Best waypoint at node "+waypoint);
			calcWaypoint=false;
		}
		GameObject walls = GameObject.Find("Walls");
		foreach(Transform wall in walls.transform){
			SpriteRenderer sprite;
			sprite=wall.GetComponent<SpriteRenderer>();
			Vector3 spriteSize = (sprite.bounds.size*0.5f);
			Debug.DrawLine(wall.position,wall.position+spriteSize,Color.magenta);

		} */
	}

	public List<int> GetNearestWaypoints(Vector3 position){
		List<int> nextWaypoints = new List<int>();
		Dictionary<int,float> waypointDistance = new Dictionary<int,float>();
		for(int i=0;i<waypoints.Length;i++){
			//Calculate distances for each point
			waypointDistance[waypoints[i]] = (graph.nodes[waypoints[i]].center-position).magnitude;
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

	//Not this one
	public int BestWaypointByArea(Vector3 position){
		int bestWaypoint = -1;
		List<int> nearestWaypoints = GetNearestWaypoints(position);
	
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

	public int BestWaypoint(Vector3 position){
		int bestWaypoint = -1;
		List<int> nearestWaypoints = GetNearestWaypoints(position);
	
		//Calculate if waypoints are in pursuers line of sight
		foreach(int waypoint in nearestWaypoints){
			bool takeWaypoint = false;
			//Ray between pursuer and waypoint
			Vector3 lineStart = pursuer.transform.position;
			Vector3 lineEnd = graph.nodes[waypoint].center;
			GameObject walls = GameObject.Find("Walls");
			//First check all walls
			foreach(Transform wall in walls.transform){
				SpriteRenderer sprite;
				sprite=wall.GetComponent<SpriteRenderer>();
				Vector3 spriteSizeVector = (sprite.bounds.size*0.5f);
				float spriteSize = Vector3.Distance(wall.position,wall.position+spriteSizeVector);
				if (HandleUtility.DistancePointLine(wall.position,lineStart,lineEnd)<=spriteSize){
					//Then this waypoint is good because there is a wall between pursuer and it
					//But first check if pursuer is out of range for waypoint
					if(Vector3.Distance(pursuer.transform.position,lineEnd)>45f){
						takeWaypoint = true;
						break;
					}
				}
			}
			//If there is no wall in sight line, check obstacles
			if (!takeWaypoint){
				GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
				foreach(GameObject obstacle in obstacles){
					SpriteRenderer sprite;
					sprite=obstacle.GetComponent<SpriteRenderer>();
					Vector3 spriteSizeVector = (sprite.bounds.size*0.5f);
					float spriteSize = Vector3.Distance(obstacle.transform.position,obstacle.transform.position+spriteSizeVector);
					if (HandleUtility.DistancePointLine(obstacle.transform.position,lineStart,lineEnd)<=spriteSize){
						//Then this waypoint is good because there is a wall between pursuer and it
						//But first check if pursuer is out of range for waypoint
						if(Vector3.Distance(pursuer.transform.position,lineEnd)>45f){
							takeWaypoint = true;
							break;
						}
					}
				}
			}
			if (takeWaypoint){
				bestWaypoint=waypoint;
				break;
			}
		}

		return bestWaypoint;
	}

	public List<int> GetNearestPursuerWaypoints(Vector3 position){
		List<int> nextWaypoints = new List<int>();
		Dictionary<int,float> waypointDistance = new Dictionary<int,float>();
		for(int i=0;i<pursuerWaypoints.Length;i++){
			//Calculate distances for each point
			waypointDistance[pursuerWaypoints[i]] = (graph.nodes[pursuerWaypoints[i]].center-position).magnitude;
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

	public int BestPursuerWaypoint(Vector3 position){
		int bestWaypoint = -1;
		List<int> nearestWaypoints = GetNearestWaypoints(position);
		float lessObstacles = Mathf.Infinity; //Number of obstacles in the way
		//Calculate if waypoints are in pursuers line of sight
		foreach(int waypoint in nearestWaypoints){
			int numObstacles=0;
			bool takeWaypoint = true;
			//Ray between pursuer and waypoint
			Vector3 lineStart = pursuer.transform.position;
			Vector3 lineEnd = graph.nodes[waypoint].center;
			GameObject walls = GameObject.Find("Walls");
			//First check all walls
			foreach(Transform wall in walls.transform){
				SpriteRenderer sprite;
				sprite=wall.GetComponent<SpriteRenderer>();
				Vector3 spriteSizeVector = (sprite.bounds.size*0.5f);
				float spriteSize = Vector3.Distance(wall.position,wall.position+spriteSizeVector);
				if (HandleUtility.DistancePointLine(wall.position,lineStart,lineEnd)<=spriteSize){
					takeWaypoint = false;
					numObstacles++;
					//break;
				}
			}
			//If there is no wall in sight line, check obstacles
			if (takeWaypoint){
				GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
				foreach(GameObject obstacle in obstacles){
					SpriteRenderer sprite;
					sprite=obstacle.GetComponent<SpriteRenderer>();
					Vector3 spriteSizeVector = (sprite.bounds.size*0.5f);
					float spriteSize = Vector3.Distance(obstacle.transform.position,obstacle.transform.position+spriteSizeVector);
					if (HandleUtility.DistancePointLine(obstacle.transform.position,lineStart,lineEnd)<=spriteSize){
						takeWaypoint = false;
						numObstacles++;
						//break;
					}
				}
			}

			if(takeWaypoint){
				bestWaypoint=waypoint;
				break;
			}
			//If the waypoint is the most "visible", select that one
			if(numObstacles<lessObstacles){
				lessObstacles=numObstacles;
				bestWaypoint=waypoint;
			}
		}

		return bestWaypoint;
	}

}