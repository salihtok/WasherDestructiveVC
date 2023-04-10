using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class WasherDestructive : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float rotSpeed; // speed of rotation;
    [SerializeField] [Range(1, 500)] private float centrifugalForce;
    [SerializeField] private Rigidbody washerRb;
    [SerializeField] private GameObject forceFront; // the force will apply washer from this object's front axis.
    [SerializeField] private GameObject forceLateral;
    [SerializeField] private GameObject lateralRotating; // the rotating part as forceFront parent.\
    [SerializeField] private GameObject frontRotating;
    
    // destructing 
    [SerializeField] [Range(10,100)] private float attachForce;
    
    [SerializeField] private bool bulletGoInside; // if thrown object from gun gets inside.
    
    [SerializeField] private GameObject topPanel;
    [SerializeField] private GameObject leftPanel;
    [SerializeField] private GameObject rightPanel;
    [SerializeField] private GameObject backPanel;
    [SerializeField] private GameObject frontPanel;
    [SerializeField] private GameObject detergentBox;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject washerDoor;

    [SerializeField] private float randomDestructionTime;

   
    
    void Start()
    {
        randomDestructionTime = Random.Range(4f, 8f);
        centrifugalForce = 0;
        rotSpeed = 0;
        washerDoor.GetComponent<Rigidbody>().isKinematic =true;
    }

   

    void FixedUpdate()
    {
       
        ObjectAttachments();
        Destructing();
        CentrifugalForce();
    }

    void CentrifugalForce()
    {
        lateralRotating.transform.Rotate(Vector3.left * rotSpeed);
        frontRotating.transform.Rotate(Vector3.forward * rotSpeed);
        
        washerRb.AddForce(forceFront.transform.up * centrifugalForce, ForceMode.Force);
        washerRb.AddForce(forceLateral.transform.up * centrifugalForce, ForceMode.Force);
    }

    void ObjectAttachments()
    {
       
        backPanel.GetComponent<Rigidbody>().AddForce(backPanel.transform.forward  * attachForce, ForceMode.Force); 
        frontPanel.GetComponent<Rigidbody>().AddForce(-frontPanel.transform.forward  * attachForce, ForceMode.Force);
        rightPanel.GetComponent<Rigidbody>().AddForce(rightPanel.transform.right   * attachForce, ForceMode.Force);
        leftPanel.GetComponent<Rigidbody>().AddForce(-leftPanel.transform.right  * attachForce, ForceMode.Force);
     
    }

    void Destructing()
    {
       
        
        if (bulletGoInside)
        {
            washerDoor.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(StartShaking());
            StartCoroutine(StartDestruction(randomDestructionTime));
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            bulletGoInside = true;
        }
    }


    IEnumerator StartDestruction(float timeForStartDestruction )
    {
        yield return new WaitForSeconds(timeForStartDestruction);
        washerDoor.GetComponent<Rigidbody>().isKinematic = false;
        attachForce = 0;
       
        bulletGoInside = false;
    }

    IEnumerator StartShaking()
    {
        yield return new WaitForSeconds(1);
       
        centrifugalForce = Random.Range(150, 350);
        rotSpeed = Random.Range(30, 90);
    }
}
