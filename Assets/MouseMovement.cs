using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour{

    public float _speed = 5f;
    public bool _move;
    public GameObject _point;
    public Vector3 _target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.z = transform.position.z;

            if (_move == false)
            {
                _move = true;
                Instantiate(_point, _target, Quaternion.identity);
            }
        }

        if (_move == true)
        {
            transform.rotation = Quaternion.FromToRotation(transform.position, _target);
            //float ang = Mathf.Atan2(-transform.position.x, transform.position.y) * Mathf.Rad2Deg;
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            //Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + steering.angular * Time.deltaTime);

            //Vector3 newDir = Vector3.RotateTowards(transform.position, _target, _speed * Time.deltaTime, 0.0f);
            //float ang = Mathf.Atan2(-transform.position.x, transform.position.y) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + ang * Time.deltaTime);
        }

    }
}

