using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ProjectileGun : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Display controls")] [SerializeField] [Range(10, 100)]
    private int linePoints;

    [SerializeField] [Range(0.01f, .25f)] private float timeBetweenPoints;

    [SerializeField] private Transform releasePosition;
    [SerializeField] [Range(1, 100)] private float throwStrength = 10f;

    [SerializeField] private GameObject gunNozzle; 

    [SerializeField] private Camera camera;

    [SerializeField] private float thrownObjectMass;
    [SerializeField] private GameObject thrownObjectPrefab;

    [SerializeField] private GameObject lineRenderedOrigin;
    
    
    // gun behavior

    [SerializeField] [Range(1, 5)] private float chargeRatio;
    [SerializeField] private bool readyToFire;
    [SerializeField] [Range(1, 3)] private float dischargeRatio;
    [SerializeField] private float chargePercent;

    public Vector3 x;
    void Start()
    {
        StartStuff();
    }

    void StartStuff()
    {
        chargePercent = 0;
        gunNozzle = GameObject.FindWithTag("nozzle");
    }

    void Update()
    {
        Inputs();
        GunChargeLogic();
        DrawProjection();
        x = gunNozzle.transform.position;
    }

    private void DrawProjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPosition = gunNozzle.transform.position;
        Vector3 startVelocity = throwStrength * gunNozzle.transform.forward/ thrownObjectMass;
        int i = 0;
        lineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            lineRenderer.SetPosition(i, point);

            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPosition,
                    (point - lastPosition).normalized,
                    out RaycastHit hit,
                    (point - lastPosition).magnitude))
            {
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }

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
    }
}