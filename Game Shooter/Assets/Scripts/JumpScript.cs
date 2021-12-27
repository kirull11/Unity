using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpScript : MonoBehaviour
{
    [SerializeField] private float jumpStrenght = 2f;
    [SerializeField] private float maxGroundDistance = 0.3f;

    private Rigidbody _rigidbody;
    private Transform _groundChackObject;

    private bool isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChackObject = GameObject.FindGameObjectWithTag("GroundCheck").transform;

    }

    private void Update()
    {
        isGrounded = Physics.Raycast(_groundChackObject.transform.position, Vector3.down, maxGroundDistance);

        if (Input.GetButtonDown("Jump") && isGrounded)
            _rigidbody.AddForce(Vector3.up * 100 * jumpStrenght);
    }
}
