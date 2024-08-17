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

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;

    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    public enum AI_STATE 
    {
        IDLE,
        PATROLLING,
        CHASING,
        ATTACKING
    };

    public AI_STATE currentState;

    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,PlayerControler.Instance.transform.position);

        switch (currentState)
        {
            case AI_STATE.IDLE:

                animator.SetBool("IsMoving", false);

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AI_STATE.PATROLLING;
                    agent.SetDestination(patrolPonts[currentPatrolPoint].position);
                }

                if(distanceToPlayer <= chaseRange)
                {
                    currentState = AI_STATE.CHASING;
                }

                break;
            case AI_STATE.PATROLLING:

                if (agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
            
                    if (currentPatrolPoint >= patrolPonts.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                   currentState = AI_STATE.IDLE;
                   waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AI_STATE.CHASING;
                }

                animator.SetBool("IsMoving", true);
                break;
            
            case AI_STATE.CHASING:

                agent.SetDestination(PlayerControler.Instance.transform.position);
                animator.SetBool("IsMoving", true);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AI_STATE.ATTACKING;
                    animator.SetTrigger("Attack");
                    animator.SetBool("IsMoving", false);

                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AI_STATE.IDLE;
                    waitCounter = waitAtPoint;

                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }

                break;

            case AI_STATE.ATTACKING:

                transform.LookAt(PlayerControler.Instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if(distanceToPlayer < attackRange)
                    {
                        animator.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currentState = AI_STATE.IDLE;
                        waitCounter = waitAtPoint;

                        agent.isStopped = false;
                    }
                }

                break;
        }

    }
}
