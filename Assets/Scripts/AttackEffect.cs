using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;
    void Start()
    {
        Destroy(gameObject, duration);
    }

}
