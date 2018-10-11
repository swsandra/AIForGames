using UnityEngine;
using UnityEditor;
using System.Collections;

public class Steering
{
    public Vector3 linear;

    //public Quaternion qOrientation;

    public float angular;

    public Steering()
    {
        linear = Vector3.zero;
        //qOrientation = new Quaternion();
        angular = 0.0f;
    }

}