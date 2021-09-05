using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    [SerializeField] private float score;
    protected AudioSource audioSource;

    //Patroling
    public Vector3 walkPoint;
    protected bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    [SerializeField] protected Vector3 gunPosition;
    
    //States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private TimeManager timeManager;

    
    protected virtual void Awake()
    {
        // player = GameObject.Find("ThirdPersonController").transform;
        agent = GetComponent<NavMeshAgent>();
        timeManager = FindObjectOfType<TimeManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timeManager && !timeManager.isRewinding)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    public virtual void Patroling()
    {
        
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    public virtual void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public virtual void AttackPlayer()
    {
        //Make sure enemy doesn't move
        
        

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Fire();

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    protected virtual void Fire()
    {
        GameObject bullet = Instantiate(projectile, gunPosition, Quaternion.identity);
        Vector3 AimPoint = new Vector3(player.position.x, player.position.y + 0.4f, player.position.z);
        bullet.transform.LookAt(AimPoint);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        
        // Removed delay for now so it feels more responsive
        //if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
        if (health <= 0) DestroyEnemy();
    }
    public virtual void DestroyEnemy()
    {
        FindObjectOfType<TimeHealth>().Health += score;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player_bullet"))
        {
            TakeDamage(1);
            other.gameObject.SetActive(false);
        }
        
        if (other.CompareTag("player_sword"))
            TakeDamage(1);
    }
}
