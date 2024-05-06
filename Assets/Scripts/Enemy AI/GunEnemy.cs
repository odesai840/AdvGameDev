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
    //bool alreadyAttacked;
    public float damage;

    //Shooting
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;

    [SerializeField] private float timer = 5;
    private float bulletTime;
    

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Attributes
    public float health = 50f;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;  // Name of player object
        agent = GetComponent<NavMeshAgent>();


        mAnimator = GetComponent<Animator>();

        mAnimator.SetBool("IsPatrolling", true);
    }

    
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        
        
        

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

        mAnimator.SetBool("IsPatrolling", true);

        //UnityEngine.Debug.Log("patrolling");
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

        mAnimator.SetBool("IsChasing", true);

        //UnityEngine.Debug.Log("Chasing");
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop the agent

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        mAnimator.SetBool("IsAttacking", true);

        if (distanceToPlayer < attackRange)
        {
            ShootAtPlayer();
        }


        //UnityEngine.Debug.Log("Shooting");
    }

    private void ResetAttack()
    {
        //alreadyAttacked = false;
    }

    void ShootAtPlayer() { 
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        Vector3 direction = (player.position - spawnPoint.position).normalized;

        
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, Quaternion.LookRotation(direction)) as GameObject;

        
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.velocity = direction * enemySpeed; 

        Destroy(bulletObj, 5f);
        
    }

    public void TakeDamage (float damage)
    {
        health -= damage; // Need dmg variable

        if (health <= 0f) Invoke("DestroyEnemy", 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
