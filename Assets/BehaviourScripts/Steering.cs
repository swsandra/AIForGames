using UnityEngine;
using UnityEditor;
using System.Collections;

public class Steering
{
    public Vector3 linear;

    public float angular;

    public Steering()
    {
        linear = Vector3.zero;
        angular = 0.0f;
    }

}