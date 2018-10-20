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
        orientation = 0.0f;
        rotation = 0.0f;
        steering = new Steering();
        Vector3 wrld = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width,0f,0f)) ;
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
        
        // Update velocity and rotation
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if (velocity.magnitude > maxSpeed){
            velocity.Normalize();
            velocity *= maxSpeed;
        }

    }

}
