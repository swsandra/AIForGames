using UnityEngine;

public class JumpPoint{

    //Jump start location and landing location
    public Vector3 jumpLocation, landingLocation;
    
    //Change in position from jump to landing
    public Vector3 deltaPosition;

    public JumpPoint(){
        jumpLocation = Vector3.zero;
        landingLocation = Vector3.zero;
    }
    public JumpPoint(Vector3 start, Vector3 end){
        jumpLocation = start;
        landingLocation = end;
        deltaPosition = landingLocation-jumpLocation;
    }

}