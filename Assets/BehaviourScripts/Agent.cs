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

    //Gravity in z axis to simulate 2.5D movement
    Vector3 gravity = new Vector3(0f, 0f, -9.8f);

    // Use this for initialization
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
        Vector3 wrld = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width,0f,0f)) ;
        groups = new Dictionary<int,List<Steering>>();
        jump=false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        if (!jump){
            // Update postition and orientation when not jumping
            transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + steering.angular * Time.deltaTime);
            Debug.Log("!jump");
        }else{
            Debug.Log("jump");
            //Perform jump
            if (transform.position.z>jumpPoint.landingLocation.z){
                transform.position+=gravity; //Apply gravity
            }
            else{ //It is in the same level as the landing pad
                transform.position = new Vector3(transform.position.x,transform.position.y,jumpPoint.landingLocation.z);
                velocity = new Vector3(velocity.x,velocity.y,0f);
                jump=false;
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
                Debug.Log("Priority: "+priority);
            }
            

        }

    }

    private Steering GetPrioritySteering(){
        Steering steering = new Steering();
        float sqrThreshold = priorityThreshold * priorityThreshold;
        foreach (List<Steering> group in groups.Values){
            steering = new Steering();
            foreach(Steering singleSteering in group){
                //Debug.Log(singleSteering.linear);
                //Debug.Log(singleSteering.angular);
                steering.linear += singleSteering.linear;
                steering.angular += singleSteering.angular;
            }
            if (steering.linear.sqrMagnitude > sqrThreshold || Mathf.Abs(steering.angular) > priorityThreshold){
                return steering;
            }

        }
        return null;
    }

    public void Jump(JumpPoint jumpPoint){
        //deltaPosition has vector between start and end of gap

        //move character till the end 

    }

}
