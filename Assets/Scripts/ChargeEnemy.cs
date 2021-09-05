using UnityEngine;

namespace DefaultNamespace
{
    public class ChargeEnemy : EnemyAI
    {
        private Animator animator;

        protected override void Awake()
        {
            base.Awake();
            
            animator = GetComponentInChildren<Animator>();
        }

        public override void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        public override void AttackPlayer()
        {
            agent.SetDestination(player.position);

            animator.SetTrigger("Charge");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
                animator.SetTrigger("Collision");
        }
    }
}