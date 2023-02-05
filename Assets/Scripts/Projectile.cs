using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    public float lifeTime = 10;
    private string sourceTag = "";
    private bool active = true;
    private AttackScriptableObject parameters;


    public void SetVelocity(Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }

    public void Initialize(GameObject source, Vector3 velocity, AttackScriptableObject parameters)
    {
        SetVelocity(velocity);
        sourceTag = source.tag;
        this.parameters = parameters;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!active || collision.collider.tag == sourceTag)
            return;
        active = false;
        Health health = collision.collider.GetComponent<Health>();
        if (health) health.Hit(parameters, transform.position.x > collision.collider.transform.position.x);
        Destroy(gameObject);
    }

}
