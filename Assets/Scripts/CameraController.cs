using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("The camera will move so that the player stays within those vales in x (In viewport space)")]
    public Vector2 minMax = Vector2.up;
    private bool followPlayer;
    private PlayerController player;
    private new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        player = PlayerController.Instance;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followPlayer)
        {
            FollowPlayer();
        }
    }

    public void ChangeMode(bool followPlayer)
    {
        this.followPlayer = followPlayer;
    }

    private void FollowPlayer()
    {
        float deltaMin = player.transform.position.x - camera.ViewportToWorldPoint(minMax.x * Vector3.right).x;
        if(deltaMin < 0)
        {
            transform.Translate(deltaMin * Vector3.right, Space.World);
        }
        float deltaMax = player.transform.position.x - camera.ViewportToWorldPoint(minMax.y * Vector3.right).x;
        if(deltaMax > 0)
        {
            transform.Translate(deltaMax * Vector3.right, Space.World);
        }
    }
    
    
}
