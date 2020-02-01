using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class cam_bound : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothtime = .5f;
    public float minzoom = 40f;
    public float maxzoom = 10f;
    public float zoomlimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }



    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        move();
        zoom();
    }
    void zoom()
    {
        float newzoom = Mathf.Lerp(maxzoom, minzoom, getgreatestdistance() / zoomlimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newzoom, Time.deltaTime);
    }


       void move()
        {
            Vector3 centerPoint = getcenterpoint();

            Vector3 newposition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newposition, ref velocity, smoothtime);
        }

    float getgreatestdistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
    Vector3 getcenterpoint()
    {
        if(targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
