using UnityEngine;

namespace DefaultNamespace
{
    public class MortarEnemy : EnemyAI
    {
        private Animator animator;
        private AnimationEventHandler events;

        [SerializeField] private Vector3 launchForce;

        protected override void Awake()
        {
            base.Awake();
            
            animator = GetComponentInChildren<Animator>();
            events = GetComponentInChildren<AnimationEventHandler>();

            if (events)
                events.OnAnimationEvent += OnAnimationEvent;
        }

        private void OnAnimationEvent(string eventName)
        {
            if (eventName == "Fire")
                FireMortar();
        }

        private void FireMortar()
        {
            var newMortarBullet = Instantiate(projectile, transform.position + gunPosition, Quaternion.identity);
            
            newMortarBullet.GetComponent<Rigidbody>().AddForce(transform.TransformPoint(launchForce));

            audioSource.Play();
        }

        protected override void Fire()
        {
            animator.SetTrigger("Firing");
        }
    }
}