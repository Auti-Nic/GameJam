using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Animator animator;
        [SerializeField] private new Collider collider;
        [SerializeField] private AnimationEventHandler events;
        
        private static readonly int Attack = Animator.StringToHash("Attack");

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            events.OnAnimationEnd += OnAnimationEnd;
        }

        private void OnDisable()
        {
            events.OnAnimationEnd -= OnAnimationEnd;
        }

        protected override void Fire()
        {
            base.Fire();
            
            animator.SetTrigger(Attack);
            
            audioSource.Play();

            collider.enabled = true;
        }

        private void OnAnimationEnd()
        {
            collider.enabled = false;
        }
    }
}