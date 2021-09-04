using UnityEngine;

namespace DefaultNamespace
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private GameObject bulletPrefab;

        protected override void Fire()
        {
            base.Fire();
            
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}