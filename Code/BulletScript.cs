using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour

{
    private Rigidbody bulletRb;
    private GameObject gunNozzle;
    [SerializeField] [Range(1, 10000)] private float speed;
    private void Awake()
    {
        gunNozzle = GameObject.FindWithTag("nozzle");
        bulletRb = GetComponent<Rigidbody>();
        bulletRb.AddForce(gunNozzle.transform.forward * speed, ForceMode.Impulse);
        
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
