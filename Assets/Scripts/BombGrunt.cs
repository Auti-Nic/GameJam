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
    
   
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    
    public override void ChasePlayer()
    {
        base.ChasePlayer();
        animator.SetBool("IsWalking", true);
    }

    
    public override void AttackPlayer()
    {
        //Make sure enemy doesn't move

        agent.SetDestination(transform.position);

        transform.LookAt(player);
        //attack code here
        
        animator.SetBool("IsAttacking", true);
        
    }

    public void Explode()
    {
        
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        FindObjectOfType<TimeHealth>().TakeDamage(explosionDamage);
        gameObject.SetActive(false);
    }

    public override void DestroyEnemy()
    {
        // TODO: Should only disable so we can go back in time.

        FindObjectOfType<TimeHealth>().Health += 10;
        animator.SetBool("IsAttacking", true);
    }


}
