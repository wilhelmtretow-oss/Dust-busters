using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_top_down_follow : MonoBehaviour
{

    public Transform player;        // Reference to the player
    private Vector3 offset;         // The start offset between player and camera
    public float smoothing = 5f;   // The catch up speed for the camera

    Vector3 newPosition;

    void Start()
    {
        // Get start offset 
        offset = transform.position - player.position;
    }


    void FixedUpdate()
    {
        // Move camera
        if (player != null)
            newPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.fixedDeltaTime);
    }
}
