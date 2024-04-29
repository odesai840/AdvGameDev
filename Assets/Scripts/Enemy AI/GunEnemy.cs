using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
//using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class GunEnemy : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private Animator mAnimator;


    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float damage;
    

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;  // Name of player object
        agent = GetComponent<NavMeshAgent>();


        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        mAnimator.SetBool("IsPatrolling", !playerInSightRange && !playerInAttackRange);
        mAnimator.SetBool("IsChasing", playerInSightRange && !playerInAttackRange);
        mAnimator.SetBool("IsAttacking", playerInSightRange && playerInAttackRange);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        // Handle ChasePlayer state
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        // Handle AttackPlayer state
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }


    }

    private void Patrolling()
    {

        //UnityEngine.Debug.Log("Patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint Reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        agent.speed = 4;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop the agent

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (angle < 60) 
            {
                transform.LookAt(player); // Look at the player when attacking

                agent.speed = 0;
                if (!alreadyAttacked)
                {
                    // Damage the player here
                    player.GetComponent<PlayerHealth>().TakeDamage(damage);
                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage (int damage)
    {
        /*
        health -= damage; // Need dmg variable

        if (health <= 0) Invoke(nameOf(DestroyEnemy), 0.5f);
        */
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


}
