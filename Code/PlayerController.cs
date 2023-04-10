using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] [Range(1,5)] private float mouseSensitivity;
    [SerializeField] [Range(1, 5)] private float movementSpeed;
    void Start()
    {
        
    }

  
    void Update()
    {
        LateralRotation();
        VerticalRotation();
        Movement();
    }

    

    void LateralRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
    }

    void VerticalRotation()
    {
        mainCamera.transform.Rotate(Input.GetAxis("Mouse Y") * mouseSensitivity, 0, 0);
    }

    void Movement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * movementSpeed*Time.deltaTime);
    }
}
