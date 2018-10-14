using UnityEngine;
using System.Collections;

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

    // Use this for initialization
    void Start()
    {
        velocity = Vector3.zero;
        //orientation = 0.0f;
        orientation = Mathf.Atan2(-transform.position.x, transform.position.y) * Mathf.Rad2Deg; // Cambie z por y
        rotation = 0.0f;
        steering = new Steering();
        Vector3 wrld = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width,0f,0f)) ;
    }

    // Update is called once per frame
    void Update()
    {
        // Update postition and orientation
        transform.position += velocity * Time.deltaTime;
        
        orientation += rotation * Time.deltaTime;

        if (orientation < 0f)
        {
            orientation = orientation + 360f;
        }

        if (orientation > 360f)
        {
            orientation = orientation - 360f;
        }
        
        // Update velocity and rotation
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if (velocity.magnitude > maxSpeed){
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (steering.angular == 0f)
        {
            rotation = 0f;
        }
        else
        {
            //transform.rotation = Quaternion.AngleAxis(orientation*Time.deltaTime, Vector3.forward);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + orientation*Time.deltaTime));
        }

    }

}
