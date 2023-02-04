using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private string sourceTag = "";
    private bool active = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!active || collision.collider.tag == sourceTag)
            return;
        active = false;

    }
}
