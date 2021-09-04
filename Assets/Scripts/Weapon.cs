using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private enum FireMode
    {
        SemiAuto,
        Burst,
        FullAuto
    }
    
    [SerializeField] private float fireRate;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private FireMode fireMode;

    [SerializeField] private int burstRounds;

    private float reloadTimer = 0;

    private int burstRoundsFired = 0;
    
    void Update()
    {
        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;
        else
            switch (fireMode)
            {
                case FireMode.SemiAuto:
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                        Fire();
                    break;
                case FireMode.Burst:
                    if (burstRoundsFired > 0 && burstRoundsFired < burstRounds)
                    {
                        Fire();
                        burstRoundsFired++;
                    } else if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Fire();
                        burstRoundsFired = 1;
                    }

                    break;
                case FireMode.FullAuto:
                    if (Input.GetKey(KeyCode.Mouse0))
                        Fire();
                    break;
            }
    }

    private void Fire()
    {
        reloadTimer = fireRate;

        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
