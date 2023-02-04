using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("How reactive the camera is. If 0, the camera won't move. If 1, it will always move at the same speed as the player")]
    [Range(0, 1)]
    public float rigidity = 0.5f;
    [Tooltip("The camera will move so that the player stays within the bounds (In viewport space)")]
    public Bounds bounds;
    public Vector3 scrollDirection = Vector3.right;
    private bool followPlayer;
    private PlayerController player;
    private new Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.Instance;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public void ChangeMode(bool followPlayer)
    {
        this.followPlayer = followPlayer;
    }

    private void FollowPlayer()
    {
        Vector3 delta = Vector3.Project(player.transform.position - transform.position, scrollDirection);
        transform.Translate(delta * rigidity);

        //if(player.transform.position.x < )
    }
}
