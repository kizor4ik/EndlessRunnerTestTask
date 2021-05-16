using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteraction : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Coin coin = other.transform.GetComponent<Coin>();
        if (coin != null)
        {
           
            SimplePool.Despawn(other.gameObject);
            DataService.AddCoin(coin.value);
        }

    }
}
