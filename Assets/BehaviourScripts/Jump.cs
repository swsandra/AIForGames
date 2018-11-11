using UnityEngine;
using System.Collections;
using UnityEditor;

public class Jump : GeneralBehaviour
{
    //Point where it can jump
    JumpPoint jumpPoint;

    //Target
    JumpPad jumpTarget;

    //Jump detection
    //public Vector3 ray, rayEnd;

    //If jump is achievable
    bool canAchieve = false;

    //Gravity in z axis to simulate 2.5D movement
    Vector3 gravity = new Vector3(0f, 0f, -9.8f); 

    // Use this for initialization
    new void Start()
    {
        base.Start();
        jumpPoint = new JumpPoint(GameObject.FindGameObjectWithTag("GapStart").transform.position,GameObject.FindGameObjectWithTag("GapEnd").transform.position);
        //GenerateRay();
        jumpTarget = null;
    }

    new void Update()
    {
        character.SetSteering(GetSteering(), weight, priority);
    }

    // Update is called once per frame
    public override Steering GetSteering()
    {
        //RotateRay();
        //Debug.DrawRay(character.transform.position, ray, Color.yellow);

        //Check if we have a trajectory, create one if not.
        if (jumpPoint!=null && jumpTarget==null){
            CalculateTarget();
        }

        //Check if trajectory is zero
        if (!canAchieve){
            //Return no steering
            character.jump=false;
            steering.linear = Vector3.zero;
            return steering;
        }

        //Check if jump point is hit
        if (Mathf.Approximately((character.transform.position-jumpTarget.position).magnitude, 0f) && Mathf.Approximately((character.velocity-jumpTarget.neededVelocity).magnitude, 0f)){
            character.jump=true;
            character.jumpPoint=jumpPoint;
            steering.linear=new Vector3(0f,0f,character.maxJumpAcc); //Salto
            return steering;
        }

        //Does velocity match?
        steering.linear = Vector3.zero;
        return steering;
    }

    //Cast rays as in collisiondetector
    /* public void GenerateRay()
    {
        float lookAhead = 4f;
        //Define point
        Vector3 centralPoint = (character.transform.position) + (Vector3.up * lookAhead);

        //Define ray
        ray = centralPoint - character.transform.position;

    }

    public void RotateRay()
    {
        //Rotate ray in direction of character movement
        ray = Vector3.RotateTowards(ray, character.velocity.normalized * ray.magnitude, character.maxSpeed * Time.deltaTime, 0f);
        rayEnd = ray + character.transform.position;

    }
 */
    //Target calculation
    protected void CalculateTarget(){

        jumpTarget.position = jumpPoint.jumpLocation;

        //Calculate the first jump time
        //Velocity after jumping
        float sqrTerm = Mathf.Sqrt(2f * gravity.z * jumpPoint.deltaPosition.z+character.maxVertSpeed*character.maxVertSpeed);
        //Flying time
        float time = (character.maxVertSpeed-sqrTerm)/gravity.z;

        //Check if we can use it
        if (!CheckJumpTime(time)){
            time = (character.maxVertSpeed+sqrTerm) / gravity.z;
            CheckJumpTime(time);
        }


    }

    private bool CheckJumpTime(float time){

        //Planar speed
        float vx = jumpPoint.deltaPosition.x / time;
        float vy = jumpPoint.deltaPosition.y / time;
        float speedSq = vx*vx+vy*vy;
        //Check if we have a valid solution
        if (speedSq<character.maxSpeed * character.maxSpeed){
            jumpTarget.neededVelocity = new Vector3(vx,vy,0f);
            canAchieve=true;
            return true;
        }
        return false;

    }

}
