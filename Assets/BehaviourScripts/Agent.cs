using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
    // MaxSpeed
    public float maxSpeed, maxVertSpeed;
    public float maxAcc, maxJumpAcc;

    // Rotation (angular velocity)
    public float maxAngularAcc, maxRotation;

    // Velocity (linear and vertical)
    public Vector3 velocity;

    // Steering
    public Steering steering;

    //Blend property and if the behaviour must be blended or not
    public bool blendProperty=false, blended=false;

    //Priority property
    public bool priorityProperty;
    public float priorityThreshold = 0.1f;
    private Dictionary<int, List<Steering>> groups;

    //Is jumping
    public bool jump;

    //Jump point 
    public JumpPoint jumpPoint;

    //Initial sprite's scale
    public Vector3 initialScale;

    // Use this for initialization
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
        groups = new Dictionary<int,List<Steering>>();
        jump=false;
        initialScale = transform.localScale;
        jumpPoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        float lastVerticalPosition = transform.position.z;
        if (!jump){
            //Cancel z movement
            velocity = new Vector3(velocity.x,velocity.y,0f);
            //transform.position = new Vector3(transform.position.x,transform.position.y,0.5f);
            
            if (jumpPoint!=null){
                transform.position = new Vector3(transform.position.x,transform.position.y,jumpPoint.landingLocation.z+0.1f);
            }
            
            // Update postition and orientation when not jumping
            transform.position += velocity * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + steering.angular * Time.deltaTime);
            
        }else{
            transform.position += velocity * Time.deltaTime;
            //Set z for jump
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+(velocity.z*Time.deltaTime));

            
            //Perform jump
            bool higher = transform.position.z-jumpPoint.landingLocation.z > 0.1; //If we are high enoughs
            if (higher){
                //transform.position+=Vector3(0f, 0f, -9.8f)*Time.deltaTime; //If gravity is applied, it automatically goes down
                //Scale progressively sprite during jump
                if (lastVerticalPosition<transform.position.z){ //Ascending
                    transform.localScale += new Vector3(0.01f, 0.01f, 0);
                }
                else if (lastVerticalPosition>transform.position.z) { //Descending
                    transform.localScale -= new Vector3(0.01f, 0.01f, 0);
                }
                //transform.localScale += new Vector3(0.1F, 0, 0); PARECIDO

            }
            else{ //It is in the same level as the landing pad
                //Debug.Log("samelevel");
                transform.position = new Vector3(transform.position.x,transform.position.y,jumpPoint.landingLocation.z+0.1f);
                velocity = new Vector3(velocity.x,velocity.y,0f);
                jump=false;
                transform.localScale=initialScale;
                //Reestablish initial sprite's scale

            }

        }

    }

    private void LateUpdate()
    {
        //Only when priority property activated
        if (priorityProperty){
            Steering newSteering = GetPrioritySteering();
            if (newSteering!=null){
                steering = newSteering;
            }
            groups.Clear();
        }
        
        //Crop to max acceleration and max angular acceleration
        if (steering.linear.magnitude > maxAcc)
        {
            steering.linear = steering.linear.normalized * maxAcc;
        }
        if (steering.angular > maxAngularAcc)
        {
            steering.angular = maxAngularAcc;
        }

        // Update velocity and rotation
        if (!jump){
            steering.linear=new Vector3(steering.linear.x,steering.linear.y, steering.linear.z);
            velocity = new Vector3(velocity.x, velocity.y, 0f);
        }
        velocity += steering.linear * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (steering.linear.Equals(Vector3.zero))
        {
            velocity = Vector3.zero;
        }

        blended = false;
    }

    public void SetSteering(Steering steer, float weight, int priority)
    {
        //Normal steering behaviour
        if (!blended && !blendProperty && !priorityProperty)
        {
            steering = steer;
            blended = true;
        }
        else
        {
            //Blend by weight
            if (!priorityProperty){
                steering.linear += (weight * steer.linear);
                steering.angular += (weight * steer.angular);
            }else{ //Priority steering
                if (!groups.ContainsKey(priority)){
                    groups.Add(priority, new List<Steering>());
                }
                groups[priority].Add(steer);
            }
            

        }

    }

    private Steering GetPrioritySteering(){
        Steering steering = new Steering();
        float sqrThreshold = priorityThreshold * priorityThreshold;
        foreach (List<Steering> group in groups.Values){
            steering = new Steering();
            foreach(Steering singleSteering in group){
                steering.linear += singleSteering.linear;
                steering.angular += singleSteering.angular;
            }
            if (steering.linear.sqrMagnitude > sqrThreshold || Mathf.Abs(steering.angular) > priorityThreshold){
                return steering;
            }

        }
        return null;
    }

}
