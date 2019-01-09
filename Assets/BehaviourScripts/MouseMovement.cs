using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour{

    public float _speed = 5f;
    public bool _move;
    //public GameObject _point;
    public Vector3 _target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //_target = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.z = transform.position.z;
            

            if (_move == false)
            {
                _move = true;
                //Instantiate(_point, _target, Quaternion.identity);
            }
        }

        if (_move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }

        Vector3 dirMov = _target - transform.position;
        if (dirMov != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, (dirMov));
        }
        

    }
}

