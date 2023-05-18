using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Slider = UnityEngine.UIElements.Slider;

public class StraightGun : MonoBehaviour
{
    [SerializeField] public Slider gunPressureSlide;
    
    [SerializeField] private Camera camera;
    [SerializeField] [Range(1, 100)] private float throwStrength = 10f;
    [SerializeField] private GameObject gunNozzle;
    [SerializeField] private GameObject thrownObjectPrefab; 
    // gun charging
    [SerializeField] [Range(1, 5)] private float chargeRatio;
    [SerializeField] private bool readyToFire;
    [SerializeField] [Range(1, 3)] private float dischargeRatio;
    [SerializeField] private float chargePercent;

   
    
    void Start()
    {
      StartStuff();  
    }
    void StartStuff()
    {
        chargePercent = 0;
        Cursor.visible = false;
      
    }
    // Update is called once per frame
    void Update()
    {
        Inputs();
        GunChargeLogic();
    }
    void Inputs()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (readyToFire)
            {
                Instantiate(thrownObjectPrefab, gunNozzle.transform.position,Quaternion.identity);
              
            }
        }

        if (Input.GetMouseButton(0))
        {
            chargePercent += chargeRatio;
        }
        else
        {
            chargePercent -= dischargeRatio;
        }
    }
    void GunChargeLogic()
    {
        
        if (chargePercent >= 10)
        {
            chargePercent = 10;
            readyToFire = true;
        }
        if (chargePercent <= 0) chargePercent = 0;
        if (chargePercent < 7) readyToFire = false;

        gunPressureSlide.value = chargePercent;
    }
}
