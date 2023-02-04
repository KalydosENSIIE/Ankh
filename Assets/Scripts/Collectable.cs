using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private bool active = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (!active || !(collision.gameObject.tag == "Player")) return;
        active = false;
        Destroy(gameObject);
        Effect(collision.gameObject);
    }

    public abstract void Effect(GameObject gameObject);
}
