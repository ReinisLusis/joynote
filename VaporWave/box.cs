using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    public GameObject FireworksAll;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
           
            Destroy(collision.gameObject);
            GameObject firework = Instantiate(FireworksAll, transform.position, Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
            
        }
    }
    /*
    void Explode()
    {
        GameObject firework = Instantiate(FireworksAll, transform.position, Quaternion.identity);
        //firework.GetComponent<ParticleSystem>().Play();
    }
    */
}
