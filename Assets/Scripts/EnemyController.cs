using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPonts;
    public int currentPatrolPoint;

    public NavMeshAgent agent;
    
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(patrolPonts[currentPatrolPoint].position);

        if (agent.remainingDistance <= .2f)
        {
            currentPatrolPoint++;
            
            if (currentPatrolPoint >= patrolPonts.Length)
            {
                currentPatrolPoint = 0;
            }

            agent.SetDestination(patrolPonts[currentPatrolPoint].position);
        }

        animator.SetBool("IsMoving", true);
    }
}
