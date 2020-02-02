using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 10;

    public float forwardSpeedModifier = 1;
    public float smooth = 5.0f;
    public float moveaway = 2f;
    public GameObject GameRoot;

    private UIManager uiManager;
    private Vector3 forwardVector;

    public GameObject FireworksAll;

    private void Start()
    {
        forwardVector = transform.forward;
        uiManager = GameRoot.GetComponent<UIManager>();

        //var colider1 = transform.Find("Colider1").GetComponent<BoxCollider>();
        //colider1.na
    }
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
        var block = other.gameObject.GetComponent<NoteBlock>();

        if (block == null || uiManager == null)
            return;

        var distance = other.transform.position.z - transform.position.z;
        if (Math.Abs(distance) > 1)
        {
            var color = other.GetComponent<MeshRenderer>().material.color;
            if (distance > 90)
            {
                color = new Color(color.r, color.g, color.b, 0.8f);
            }
            else if (distance > 45)
            {
                color = new Color(color.r, color.g, color.b, 0.5f);
            }
            else if (distance < -5)
            {
                color = new Color(color.r, color.g, color.b, 0.05f);
            }

            other.GetComponent<MeshRenderer>().material.color = color;

            return;
        }

        GameObject firework = Instantiate(FireworksAll, other.transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();

        uiManager.UpdateScore(block.IsGoodBlock);

        var audioManager = GameRoot.GetComponent<AudioManager>();
        if (audioManager != null)
        {
            audioManager.BlockHit(block);
        }

        Destroy(other.gameObject);
    }

    internal void UpdateForwardPosition(float time)
    {
        forwardVector = transform.forward * 100 * time;
    }
}



