using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoosterInteraction : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        SpeedBoost booster = other.transform.GetComponent<SpeedBoost>();
        if (booster != null)
        {
            player.SpeedUp(booster.speedFactor);
        }
    }
}
