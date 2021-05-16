using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInteraction : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            player.ActionPlayer();
        }

    }
}
