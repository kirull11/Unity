using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float rotationSpeed = 10f;
    private Vector3 _direction;
    void Start()
    {
        
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        //Debug.Log(_direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * rotationSpeed);
    }

    void FixedUpdate()
    {
        transform.position = transform.position + _direction * Time.fixedDeltaTime * Speed;
    }
}
