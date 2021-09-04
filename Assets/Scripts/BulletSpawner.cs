using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SpawnBullet();
    }

    private void SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
    }
}
