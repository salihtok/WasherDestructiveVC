using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChanger : MonoBehaviour
{
    [SerializeField] private GameObject gunPosition; // using gun position\
    [SerializeField] private GameObject changePosition; //when gun changes it goes down and other goes gun position.
    [SerializeField] private GameObject gunOne;
    [SerializeField] private GameObject gunTwo;
    public float mouseScrollY;
    public bool one;
    public bool two;
    void Start()
    {
        one = true;
        two = false;
    }

    // Update is called once per frame
    void Update()
    {
     MouseController();   
     GunController();
    }

    void MouseController()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            one = !one;
            two = !two;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            one = !one;
            two = !two;
        }
    }

    void GunController()
    {
        if (one)
        {
            gunOne.SetActive(true);
            gunTwo.SetActive(false);
        }

        if (two)
        {
            
            gunOne.SetActive(false);
            gunTwo.SetActive(true);
        }
    }
    
}
