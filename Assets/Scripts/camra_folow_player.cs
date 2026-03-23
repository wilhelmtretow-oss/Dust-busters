using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_top_down_follow : MonoBehaviour
{
    public Transform player;
    public float smoothing = 5f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothing * Time.deltaTime);
        }
    }
}