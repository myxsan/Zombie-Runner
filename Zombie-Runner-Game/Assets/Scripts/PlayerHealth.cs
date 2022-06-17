using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHitPoints = 100f;

    public void PlayerTakeDamage(float damage)
    {
        playerHitPoints -= damage;
        Debug.Log(playerHitPoints);

        if(playerHitPoints <= 0 )
        {
            Debug.Log("Player Death");
        }
    }

}
