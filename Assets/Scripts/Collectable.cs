using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private bool active = true;
    private void OnTriggerEnter(Collider collision)
    {
        if (!active || !(collision.tag == "Player")) return;
        active = false;
        Destroy(gameObject);
        Effect(collision.gameObject);
    }

    public abstract void Effect(GameObject gameObject);
}
