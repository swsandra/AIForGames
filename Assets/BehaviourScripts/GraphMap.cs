using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GraphMap : MonoBehaviour{

    //List of Nodes
    public Dictionary<int,Node> nodes;

    //List of Connections
    public List<Connection> connections;

    private GameObject floor;
    private SpriteRenderer spriteRenderer;

    public bool drawTriangles, drawConnections;

    //Read map, add nodes and connections at start
    void Start(){
        nodes = new Dictionary<int,Node>();
        connections = new List<Connection>();
        GetTriangles();
        SetWalls();
        GetConnections();
        drawConnections=true;
        drawTriangles=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(drawTriangles){
            foreach(KeyValuePair<int, Node> entry in nodes)
            {
                entry.Value.DrawTriangle();
            }
        }

        if(drawConnections){
            foreach(Connection con in connections){
                con.DrawConnection();
            }
        }        
    }

    public void GetTriangles(){
        //Get floor object
        floor = GameObject.Find("Floor");
        int i = 0;
        //Find vertex of each child 
        foreach (Transform child in floor.transform){
            spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();
            Sprite plank = spriteRenderer.sprite;
            //Create 2 nodes and add to graph
            ushort[]  t = plank.triangles;
            // draw the triangles using grabbed vertices
            int a,b,c;
            Vector2[] aux = plank.vertices;
            Vector3[] v = new Vector3[aux.Length];
            for(int z = 0; z < aux.Length; z++){
                v[z]=aux[z];
            }
            for (int j = 0; j < t.Length; j = j + 3)
            {
                Vector3[] vertices = new Vector3[3];
                a = t[j];
                b = t[j + 1];
                c = t[j + 2];
                
                vertices[0]=v[a]+child.gameObject.transform.position;
                vertices[1]=v[b]+child.gameObject.transform.position;
                vertices[2]=v[c]+child.gameObject.transform.position;
                AddNode(vertices,i++);
                
            }
        }
    }

    public void SetWalls(){
        foreach (Transform child in floor.transform){
            child.gameObject.tag="Obstacle";
        }
    }

    public void GetConnections(){
        for (int i = 0; i<nodes.Count; i++){
            for (int j = i+1; j<nodes.Count; j++){
                //Check if any pair of nodes shares a side
                if (CheckSide(nodes.ElementAt(i).Value,nodes.ElementAt(j).Value)){
                    AddConnection(nodes.ElementAt(i).Value,nodes.ElementAt(j).Value);
                }
            }
        }
    }

    public bool CheckSide(Node node, Node node1){
        //Check if two triangle nodes share a side
        for(int i = 0; i<node.vertex.Length; i++){
            for(int j = 0; j <node1.vertex.Length; j++){
                //Calculate middle point of each pair of vertex
                float x1=(node.vertex[i%node.vertex.Length].x + node.vertex[(i+1)%node.vertex.Length].x)/2;
                float y1=(node.vertex[i%node.vertex.Length].y + node.vertex[(i+1)%node.vertex.Length].y)/2;

                float x2=(node1.vertex[j%node1.vertex.Length].x + node1.vertex[(j+1)%node1.vertex.Length].x)/2;
                float y2=(node1.vertex[j%node1.vertex.Length].y + node1.vertex[(j+1)%node1.vertex.Length].y)/2;
                            
                if(Mathf.Abs(x1-x2)<1f & Mathf.Abs(y1-y2)<1f){
                    return true;
                }
            }
        }
        return false;
    }

    public bool AddNode(Vector3[] vertex, int id){

        if (nodes.ContainsKey(id)){
            return false;
        }
        nodes.Add(id,new Node(vertex,id));
        return true;

    }

    public bool AddTestNode(Node node){

        if (nodes.ContainsKey(node.id)){
            return false;
        }
        nodes.Add(node.id,node);
        return true;

    }

    //Add Connection
    public bool AddConnection(Node initial, Node final){

        foreach (Connection con in connections){
            if ((con.initialNode.id==initial.id && con.finalNode.id==final.id) || (con.initialNode.id==final.id && con.finalNode.id==initial.id)){
                return false;
            }
        }

        connections.Add(new Connection(initial, final));
        return true;

    }

    //Information for each node
    public struct NodeRecord{

        public Node node;
        public Connection connection;
        public float costSoFar;
        public float estimatedTotalCost;

        public NodeRecord(Node node, Connection connection, float costSoFar, float estimatedTotalCost){
            this.node=node;
            this.connection=connection;
            this.costSoFar=costSoFar;
            this.estimatedTotalCost=estimatedTotalCost;
        }

        public Node GetFromNode(){
            if (connection.initialNode.id==node.id){
                return connection.finalNode;
            }else{
                return connection.initialNode;
            }
        }

    }

    public List<Node> AStar(Node start, Node end){

        List<Node> path = new List<Node>();
        //path.Add(start);

        NodeRecord startRecord = new NodeRecord(start, null, 0f, EuclideanDistance(start, end));

        Dictionary<Node, NodeRecord> open = new Dictionary<Node, NodeRecord>();
        Dictionary<Node, NodeRecord> closed = new Dictionary<Node, NodeRecord>();

        open.Add(start, startRecord);
        NodeRecord current = new NodeRecord();

        while(open.Count>0f){
            current = SmallestElement(open);
            
            if (current.node.Equals(end)){
                //return path;
                break;
            }

            List<Connection> currentConnections = GetNodeConnections(current.node);

            foreach(Connection con in currentConnections){
                Node endNode;
                if (con.initialNode.id==current.node.id){
                    endNode = con.finalNode;
                }else{
                    endNode = con.initialNode;
                }
                
                float endNodeCost = current.costSoFar + con.cost;
                
                NodeRecord endNodeRecord;
                float endNodeHeuristic;

                if (closed.ContainsKey(endNode)){
                    endNodeRecord = closed[endNode];
                    if (endNodeRecord.costSoFar<=endNodeCost){
                        continue;
                    }
                    closed.Remove(endNode);
                    endNodeHeuristic = endNodeRecord.connection.cost - endNodeRecord.costSoFar;

                }else if (open.ContainsKey(endNode)){
                    endNodeRecord = open[endNode];
                    if (endNodeRecord.costSoFar<=endNodeCost){
                        continue;
                    }
                    endNodeHeuristic = endNodeRecord.connection.cost - endNodeRecord.costSoFar;

                }else{
                    endNodeRecord = new NodeRecord(endNode, null, 0f, 0f);
                    endNodeHeuristic = EuclideanDistance(start, endNode); 
                }
                endNodeRecord.connection=con;
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;

                if (!open.ContainsKey(endNode)){
                    open.Add(endNode,endNodeRecord);
                }
            }

            open.Remove(current.node);
            closed.Add(current.node, current);

        }

        if (current.node.id != end.id){
            return null;
        }else{
            //Starts with current node
            Node fromNode=current.node;
            NodeRecord fromNodeRecord=current;
            while (fromNode.id!=start.id){
                path.Insert(0,fromNode);
                //Change fromNode
                fromNode = fromNodeRecord.GetFromNode();
                //Search in closed 
                fromNodeRecord = closed[fromNode];
            }
            path.Insert(0,fromNode); //start node

        }
        return path;

    }

    public static float EuclideanDistance(Node start, Node end){

        Vector3 startCenter = start.center;
        Vector3 endCenter = end.center;
        float square = (startCenter.x - endCenter.x) * (startCenter.x - endCenter.x) + (startCenter.y - endCenter.y) * (startCenter.y - endCenter.y);
        return square;
    }

    public NodeRecord SmallestElement(Dictionary<Node, NodeRecord> records){

        NodeRecord smallest=records.First().Value;
        foreach(KeyValuePair<Node, NodeRecord> record in records){
            if (record.Value.estimatedTotalCost<smallest.estimatedTotalCost){
                smallest=record.Value;
            }
        }
        return smallest;

    }

    public List<Connection> GetNodeConnections(Node node){

        List<Connection> nodeConnections = new List<Connection>();

        foreach(Connection con in connections){
            if (con.finalNode.id==node.id || con.initialNode.id==node.id){
                nodeConnections.Add(con);
            }
        }
        
        return nodeConnections;
    }

    

}