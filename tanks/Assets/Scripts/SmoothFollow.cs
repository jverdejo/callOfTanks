using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3F;
    public Vector3 offset;
    public bool autoOffset;


    private void Start()
    {
        if (autoOffset)
        {
            offset = transform.position - target.position;
        }
    }
    void FixedUpdate()
    {
        if (target == null)
            return;
        Vector3 posicionObjetivo = target.position + offset;
        Vector3 posicionSmooth = Vector3.Lerp(transform.position,posicionObjetivo,smoothTime);
        transform.position = posicionSmooth;

        }

    
}
