using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    
    // TODO: Reference to the player / camera
    [SerializeField] private Transform player;
    
    // TODO: List of spawned enemies

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private float waveTimer = 0;
    private float timeBetweenWaves = 5;

    [SerializeField] private float spawnRange;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        // TODO: If previous wave completed, wait and then spawn a new wave
        // TODO: Needs to be affected by reversal
        if (waveTimer < timeBetweenWaves)
            waveTimer += Time.deltaTime;
        else
        {
            waveTimer = 0;
            SpawnWave();
        }
    }
    
    // TODO: Function to spawn a wave
    private void SpawnWave()
    {
        // TODO: More than one spawning location
        Vector3 spawnLocation = RandomPointInCircle();
        
        // TODO: Spawn more than one enemy
        // TODO: Should probably spawn facing towards the player
        var newEnemy = Instantiate(enemyPrefabs[0], spawnLocation, Quaternion.identity);
        spawnedEnemies.Add(newEnemy);

        newEnemy.GetComponent<EnemyAI>().player = player;
    }

    private Vector3 RandomPointInCircle()
    {
        // TODO: Should select a point outside the camera's view
        float r = spawnRange * Mathf.Sqrt(Random.value);
        float theta = Random.value * 2 * Mathf.PI;

        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        return player.position + new Vector3(x, 0, y);
    }
}
