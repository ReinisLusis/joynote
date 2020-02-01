﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 10;

    public float forwardSpeedModifier = 1;
    public float smooth = 5.0f;
    public float moveaway = 1.5f;

    private Vector3 forwardVector;
    
    void Update()
    {
        if (forwardVector != null)
        {
            transform.position = forwardVector;
        }

        transform.position = new Vector3(Input.GetAxis("Horizontal") * moveaway, 
            -Input.GetAxis("Vertical") * moveaway, transform.position.z);

        float tiltAroundZ = -Input.GetAxis("Horizontal") * moveaway;
        float tiltAroundX = Input.GetAxis("Vertical") * moveaway;

        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Block")
        {
            Destroy(other.gameObject);
        }
    }

    internal void UpdateForwardPosition(float time)
    {
        forwardVector = transform.forward * forwardSpeedModifier * time;
        Debug.Log(string.Format("t{0} dt{1}",time, Time.deltaTime));
    }
}



