using UnityEngine;
using System.Collections;

public class GeneralBehaviour : MonoBehaviour
{

    public Agent character;
    public Agent target;
    public Steering steering;
    public float weight = 1f;
    public int priority = 1;

    public bool stop = false;

    // Use this for initialization
    protected void Start()
    {
        steering = new Steering();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(!stop){
            character.SetSteering(GetSteering(), weight, priority);
        }else{
            character.SetSteering(new Steering(), weight, priority);
        }
    }

    public virtual Steering GetSteering()
    {
        return steering;
    }

    public float GetNewOrientation(float currentOrientation, Vector3 velocity)
    {
        if (velocity.magnitude > 0)
        {
            return Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg; // Cambie z por y
        }
        return currentOrientation;
    }

    //2D
    protected Vector3 GetOrientationAsVector(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Cos(orientation*Mathf.Deg2Rad) * 1.0f;
        vector.y = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.Normalize();
        return vector;
    }

    //Map the parameter to (-180, 180) interval
    public float MapToRange(float rotValue)
    {
        rotValue = rotValue % 360f;
        if (rotValue < -180f)
        {
            return rotValue + 360f;
        }
        else if (rotValue > 180f)
        {
            return rotValue - 360f;
        }
        return rotValue;
    }

}
