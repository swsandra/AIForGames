using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
    // MaxSpeed
    public float maxSpeed;
    public float maxAcc;

    // Orientation 
    public float orientation;

    // Rotation (angular velocity)
    public float rotation, maxAngularAcc, maxRotation;

    // Velocity
    public Vector3 velocity;

    // Steering
    public Steering steering;

    //Blend property and if the behaviour must be blended or not
    public bool blendProperty=false, blended=false;

    //Priority property
    public bool priorityProperty;

    public float priorityThreshold = 0.2f;
    private Dictionary<int, List<Steering>> groups;

    // Use this for initialization
    void Start()
    {
        velocity = Vector3.zero;
        orientation = 0.0f;
        rotation = 0.0f;
        steering = new Steering();
        Vector3 wrld = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width,0f,0f)) ;
        groups = new Dictionary<int,List<Steering>>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update postition and orientation
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + steering.angular * Time.deltaTime);

        orientation += rotation * Time.deltaTime;

        if (orientation < 0f)
        {
            orientation = orientation + 360f;
        }

        if (orientation > 360f)
        {
            orientation = orientation - 360f;
        }

        if (steering.angular > maxAngularAcc)
        {
            steering.angular = maxAngularAcc;
        }

    }

    private void LateUpdate()
    {
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
        rotation += steering.angular * Time.deltaTime;

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

    public void SetSteering(Steering steer, float weight)
    {

        if (!blended && !blendProperty)
        {
            steering = steer;
            blended = true;
        }
        else
        {
            steering.linear += (weight * steer.linear);
            steering.angular += (weight * steer.angular);

            /*if (steering.linear.magnitude > maxAcc)
            {
                steering.linear = steering.linear.normalized * maxAcc;
            }
            if (steering.angular > maxAngularAcc)
            {
                steering.angular = maxAngularAcc;
            }*/
        }

    }

}
