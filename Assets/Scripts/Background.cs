using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera cam;
    private float length;
    private float initialX;
    private void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main;
        initialX = transform.position.x;
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        if (pos.x - initialX - cam.transform.position.x >= length)
            pos.x -= length;
        if (cam.transform.position.x - pos.x + initialX >= length)
            pos.x += length;
        transform.position = pos;
    }
}
