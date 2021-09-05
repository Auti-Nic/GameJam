using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombGrunt : EnemyAI
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Patroling()
    {
        animator.SetBool("IsWalking", true);
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

    public override void ChasePlayer()
    {
        animator.SetBool("IsWalking", true);
        agent.SetDestination(player.position);
    }

    public override void AttackPlayer()
    {
        //Make sure enemy doesn't move

        agent.SetDestination(transform.position);

        transform.LookAt(player);
        //attack code here
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", true);
        
    }

    
}
