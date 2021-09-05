using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BombGrunt : EnemyAI
{

    // Start is called before the first frame update
 




    //Animator 
    private Animator animator;

    //explosion 
    public GameObject explosionPrefab;
    public int explosionDamage = 5;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame



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

    public override void DestroyEnemy()
    {
        GameObject.Find("ScoreBoard").GetComponent<ScoreScript>().scoreValue += 5;
        Destroy(gameObject);
    }

 
   
}
