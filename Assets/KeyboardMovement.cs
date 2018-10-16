using UnityEngine;
using System.Collections;

public class KeyboardMovement : MonoBehaviour
{
    float movementSpeed = 5f;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime, Input.GetAxis("Vertical") * Time.deltaTime, 0f);
    }

}
