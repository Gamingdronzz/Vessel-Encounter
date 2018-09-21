﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatObject : MonoBehaviour
{

    public float waterLevel = 0.0f;

    public float floatThreshold = 2.0f;

    public float waterDensity = 0.125f;

    public float downForce = 4.0f;

    private float forceFactor;

    private Vector3 floatForce;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);

        if (forceFactor > 0.0f)
        {

            floatForce = -Physics.gravity * rb.mass * (forceFactor - rb.velocity.y * waterDensity);

            floatForce += new Vector3(0.0f, -downForce * rb.mass, 0.0f);

            rb.AddForceAtPosition(floatForce, transform.position);

        }

    }

}