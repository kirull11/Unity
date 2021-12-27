using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float MouseSensivity = 100f;
    [SerializeField] private Transform PlayerBody;

    float xRotation = 0f;

   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

   
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 130f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);

    }
}
