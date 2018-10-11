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
        //orientation = Mathf.Atan2(transform.position.x, transform.position.y) * Mathf.Rad2Deg; // Cambie z por y
        rotation = 0.0f;
        steering = new Steering();
    }

    // Update is called once per frame
    void Update()
    {
        // Update postition and orientation
        transform.position += velocity * Time.deltaTime;
        //float initialOrientation = orientation;
        orientation += rotation * Time.deltaTime;

        //print(orientation);
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + orientation*Time.deltaTime));
        //transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + orientation));  //un trompo

        // Update velocity and rotation
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if (velocity.magnitude > maxSpeed){
            velocity.Normalize();
            velocity *= maxSpeed;
        }

    }

}
