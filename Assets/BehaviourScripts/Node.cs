using UnityEngine;
using System.Collections;

public class Node{

    //Triangle vertex
    public Vector3[] vertex=new Vector3[3];
    
    //Node id
    public int id;

    //Triangle center
    public Vector3 center;

    public Node(Vector3[] vertex, int id){
        this.vertex=vertex;
        this.id=id;
        this.center=GetCenter();
    }

    public Node(Vector3 center, int id){
        this.center=center;
        this.vertex=null;
        this.id=id;
    }

    //Calculate center of triangle
    public Vector3 GetCenter(){
        
        float x = (vertex[0].x+vertex[1].x+vertex[2].x)/3;
        float y = (vertex[0].y+vertex[1].y+vertex[2].y)/3;
        return new Vector3(x,y,0f);

    }

    public void DrawTriangle(){
        
        Debug.DrawLine(vertex[0],vertex[1],Color.green);
        Debug.DrawLine(vertex[1],vertex[2],Color.green);
        Debug.DrawLine(vertex[2],vertex[0],Color.green);

    }

}