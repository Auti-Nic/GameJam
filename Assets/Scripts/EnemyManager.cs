using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private float difficulty;
    [SerializeField] private float difficultyIncreaseSpeed;
    
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
        difficulty += difficultyIncreaseSpeed * Time.deltaTime;

        timeBetweenWaves = 1 / difficulty;
        
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

        var randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        var newEnemy = Instantiate(randomEnemy, spawnLocation, Quaternion.identity);
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
