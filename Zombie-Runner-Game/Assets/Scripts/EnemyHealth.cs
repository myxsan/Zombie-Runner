using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    
    bool isDead = false;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;

        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if(isDead) { return; }

        GetComponent<Animator>().SetTrigger("Die");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;

        Destroy(gameObject, 10f);
    }
}
