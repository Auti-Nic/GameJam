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
            
            collider.enabled = true;
        }

        private void OnAnimationEnd()
        {
            collider.enabled = false;
        }
    }
}