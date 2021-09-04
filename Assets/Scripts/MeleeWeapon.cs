using UnityEngine;

namespace DefaultNamespace
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Attack = Animator.StringToHash("Attack");

        protected override void Fire()
        {
            base.Fire();
            
            animator.SetTrigger(Attack);
        }
    }
}