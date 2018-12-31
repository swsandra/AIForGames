using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GraphPathFollowing : GeneralBehaviour
{

    //Graph
    GraphMap graph;
    public int initial, end, current;

    //Vector target position
    Vector3 targetPosition;
    
    //Radius to change from nodes, target radius and slow radius
    public float changeRadius = 2f, tRadius= 2f, sRadius = 3f, timeToTarget = 0.2f;

    //Script for final node
    GeneralBehaviour behaviour;

    //Path to follow
    List<Node> path;

    public float delta = 2f;

    public GameObject astar_target;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        //Get map graph
        GameObject map = GameObject.Find("Map Graph");
        graph = map.GetComponent<GraphMap>();
        initial = graph.GetNearestNodeByCenter(character.transform.position);
        end = graph.GetNearestNodeByCenter(target.transform.position);
        path = graph.AStar(graph.nodes[initial],graph.nodes[end]);
        behaviour = GetComponent<DArrive>();
        current = path[0].id;
        path.RemoveAt(0);
    }

    // Update is called once per frame
    new void Update()
    {
        int targetNode = graph.GetNearestNodeByCenter(target.transform.position);
        int characterNode = graph.GetNearestNodeByCenter(character.transform.position);
        graph.nodes[initial].DrawTriangle();
        graph.nodes[end].DrawTriangle();
        //IMPORTANTE
        if (targetNode!=characterNode && targetNode!=end){
            
            if(characterNode!=-1){
                initial = characterNode;
            }

            if(targetNode!=-1){
                ChangeEndNode(targetNode);
                path = graph.AStar(graph.nodes[initial],graph.nodes[end]);
                path.RemoveAt(0);
                current = path[0].id;
                path.RemoveAt(0);
            }
                  
        }
        
        character.SetSteering(GetSteering(path), weight, priority); //!!!!After arriving at final node, needs behaviour activation
    }

    public Steering GetSteering(List<Node> path)
    {
        //If it is at a certain radius from last current, change 
        Vector3 distanceToNode = graph.nodes[current].center-character.transform.position;
        if (distanceToNode.magnitude<changeRadius && path.Count!=0){
            //Remove first element
            current = path[0].id;
            path.RemoveAt(0);
            //If path is not empty, seek
            if (path.Count!=0){
                targetPosition = graph.nodes[current].center;
                steering.linear = targetPosition - character.transform.position;
                steering.linear.Normalize();
                steering.linear *= character.maxAcc;
                steering.angular = 0f;
            }
            else{ //else arrive at current node
                targetPosition = graph.nodes[current].center;
                Vector3 direction = targetPosition - character.transform.position; 
                float distance = direction.magnitude; 
                float tSpeed;

                if (distance < tRadius)
                {
                    steering.linear = Vector3.zero; 
                    character.velocity = Vector3.zero;    
                    return steering;
                }

                if (distance > sRadius)
                {
                    tSpeed = character.maxSpeed;
                }
                else
                {
                    tSpeed = character.maxSpeed * (distance / sRadius);
                }
                Vector3 tVelocity = direction.normalized * tSpeed;

                steering.linear = tVelocity - character.velocity;
                
                steering.linear /= timeToTarget;

                if (steering.linear.magnitude > character.maxAcc)
                {
                    steering.linear.Normalize();
                    steering.linear *= character.maxAcc;
                }

                steering.angular = 0f;
            }
        }
        else{//else seek current
            if(current!=end){ //seek last current
                targetPosition = graph.nodes[current].center;
                steering.linear = targetPosition - character.transform.position;
                steering.linear.Normalize();
                steering.linear *= character.maxAcc;
                steering.angular = 0f;
            }else{ //arrive at final node
                targetPosition = graph.nodes[current].center;
                Vector3 direction = targetPosition - character.transform.position; 
                float distance = direction.magnitude; 
                float tSpeed;

                if (distance < tRadius)
                {
                    steering.linear = Vector3.zero; 
                    character.velocity = Vector3.zero;    
                    return steering;
                }

                if (distance > sRadius)
                {
                    tSpeed = character.maxSpeed;
                }
                else
                {
                    tSpeed = character.maxSpeed * (distance / sRadius);
                }
                Vector3 tVelocity = direction.normalized * tSpeed;

                steering.linear = tVelocity - character.velocity;
                
                steering.linear /= timeToTarget;

                if (steering.linear.magnitude > character.maxAcc)
                {
                    steering.linear.Normalize();
                    steering.linear *= character.maxAcc;
                }

                steering.angular = 0f;
            }
            
        }
        return steering;

    }

    //To use this, GetComponent
    public void ChangeEndNode(int node){
        initial = graph.GetNearestNodeByCenter(character.transform.position);
        end = node;
        if(initial!=-1){
            path = graph.AStar(graph.nodes[initial],graph.nodes[end]);
        }
        
    }

    public int GetNearestNodeByAreas(Vector3 position, float delta){
        
        int nearestNode=-1;

        for(int i = 0; i<graph.nodes.Count ; i++){
            Vector3 A = graph.nodes[i].vertex[0];
            Vector3 B = graph.nodes[i].vertex[1];
            Vector3 C = graph.nodes[i].vertex[2];
            //Calculate areas
            float ABC = (1f/2f)* Mathf.Abs(((A.x-C.x)*(B.y-A.y))-((A.x-B.x)*(C.y-A.y)));
            float ABP = (1f/2f)* Mathf.Abs(((A.x-position.x)*(B.y-A.y))-((A.x-B.x)*(position.y-A.y)));
            float BCP = (1f/2f)* Mathf.Abs(((B.x-position.x)*(C.y-B.y))-((B.x-C.x)*(position.y-B.y)));
            float ACP = (1f/2f)* Mathf.Abs(((A.x-position.x)*(C.y-A.y))-((A.x-C.x)*(position.y-A.y)));
            float sumOfAreas = ABP + BCP + ACP;
            //if (sumOfAreas <= ABC | sumOfAreas <= nearestTriangleArea){
            if (sumOfAreas <= delta){
                return graph.nodes[i].id;
            }
        }
        return nearestNode;
    }

}