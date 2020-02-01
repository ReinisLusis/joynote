using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
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
        Vector3 centerPoint = target.position;

        Vector3 newposition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newposition, ref velocity, smoothtime);
    }

    float getgreatestdistance()
    {
        var bounds = new Bounds(target.position, Vector3.zero);
        bounds.Encapsulate(target.position);
        return bounds.size.x;
    }
}
