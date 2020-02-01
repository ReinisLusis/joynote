using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    
    public float movespeed = 10;
    public float forwardspeed;
    public float smooth = 5.0f;
    public float moveaway = 1.5f;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * forwardspeed;

        transform.position = new Vector3(Input.GetAxis("Horizontal") * moveaway , -Input.GetAxis("Vertical") * moveaway, transform.position.z);

        //transform.Translate(movespeed * Input.GetAxis("Horizontal") * Time.deltaTime, -movespeed * Input.GetAxis("Vertical") * Time.deltaTime, 0f);


        float tiltAroundZ = -Input.GetAxis("Horizontal") * moveaway;
        float tiltAroundX = Input.GetAxis("Vertical") * moveaway;


        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}



