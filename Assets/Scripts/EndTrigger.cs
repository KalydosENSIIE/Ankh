using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    bool started = false;
    private CameraController camController;


    private void Start()
    {
        camController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (!started)
        {
            if (camController.transform.position.x > transform.position.x)
            {
                started = true;
                GameManager.Instance.EndGame();
            }
            return;
        }
    }
}
