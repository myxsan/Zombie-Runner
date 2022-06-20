using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float trunSpeed = 5f;

    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;

    private NavMeshAgent navMeshAgent;
    private Transform target;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update()
    {
        ChaseProcess();
    }

    private void ChaseProcess()
    {
        distanceToTarget = Vector3.Distance(target.position, this.transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();

        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetTrigger("Move");
        GetComponent<Animator>().SetBool("Attack", false);

        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * trunSpeed);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, chaseRange);
    }
}
