using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 10;

    public float forwardSpeedModifier = 1;
    public float smooth = 5.0f;
    public float moveaway = 1.5f;
    public GameObject GameRoot;

    private UIManager uiManager;
    private Vector3 forwardVector;

    public GameObject FireworksAll;

    private void Start()
    {
        forwardVector = transform.forward;
        uiManager = GameRoot.GetComponent<UIManager>();
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
        GameObject firework = Instantiate(FireworksAll, other.transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();

        if (uiManager)
        {
            var block = other.gameObject.GetComponent<NoteBlock>();
            if (block != null)
            {
                uiManager.UpdateScore(block.IsGoodBlock);

                var audioManager = GameRoot.GetComponent<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.BlockHit(block);
                }
            }
        }
        Destroy(other.gameObject);
    }

    internal void UpdateForwardPosition(float time)
    {
        forwardVector = transform.forward * 100 * time;
    }
}



