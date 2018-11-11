using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JumpAI : VelocityMatch
{
    //public JumpPoint[] jumpPoints;
    public JumpPoint jumpPoint;
    GameObject jumpTarget;
    
    //If jump is achievable
    bool canAchieve = false;
    
    public float maxVerticalVelocity;

    //Gravity in z axis to simulate 2.5D movement
    public Vector3 gravity = new Vector3(0f, 0f, -9.8f); 

    public GeneralBehaviour[] behaviours;

    public Projectile projectile;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        //this.enabled=false;
        projectile = gameObject.AddComponent<Projectile>();
        GeneralBehaviour[] abs = gameObject.GetComponents(typeof(GeneralBehaviour)) as GeneralBehaviour[];

        behaviours = new GeneralBehaviour[abs.Length-1];
        int i=0;
        foreach(GeneralBehaviour b in abs){
            if (b==this){
                continue;
            }
            behaviours[i]=b;
            i+=1;
        }

        jumpPoint = new JumpPoint(GameObject.FindGameObjectWithTag("GapStart").transform.position,GameObject.FindGameObjectWithTag("GapEnd").transform.position);

    }

    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    // Update is called once per frame
    public override Steering GetSteering()
    {
        //Check if we have a trajectory, create one if not.
        if (jumpPoint!=null && jumpTarget==null){
            CalculateTarget();
        }

        if (!canAchieve){
            return steering;
        }

        //Check if jump point is hit
        if (Mathf.Approximately((character.transform.position-jumpTarget.transform.position).magnitude, 0f) && Mathf.Approximately((character.velocity-jumpTarget.GetComponent<Agent>().velocity).magnitude, 0f)){
            DoJump();
            return steering;
        }
        return base.GetSteering();
    }

    //Disables all behaviours except jumping
    public void Isolate(bool state){
        foreach (GeneralBehaviour behaviour in behaviours){
            behaviour.enabled=!state;
        }
        this.enabled=state;
    }

    public void DoJump(){
        
        projectile.enabled = true;
        Vector3 direction;
        direction = Projectile.GetFireDirection(jumpPoint.jumpLocation, jumpPoint.landingLocation, character.maxSpeed);
        projectile.Set(jumpPoint.jumpLocation, direction, character.maxSpeed, false);
        
    }

    protected void CalculateTarget(){

        jumpTarget = new GameObject();
        jumpTarget.AddComponent<Agent>();

        //Calculate the first jump time
        float sqrTerm = Mathf.Sqrt(2f * gravity.z * jumpPoint.deltaPosition.z+maxVerticalVelocity*character.maxSpeed);
        float time = (maxVerticalVelocity-sqrTerm)/gravity.z;

    }

    private bool CheckJumpTime(float time){

        //Planar speed
        float vx = jumpPoint.deltaPosition.x / time;
        float vy = jumpPoint.deltaPosition.y / time;
        float speedSq = vx*vx+vy*vy;
        //Check if we have a valid solution
        if (speedSq<character.maxSpeed * character.maxSpeed){
            jumpTarget.GetComponent<Agent>().velocity = new Vector3(vx,vy,0f);
            canAchieve=true;
            return true;
        }
        return false;

    }

}