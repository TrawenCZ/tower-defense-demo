using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemySpawner : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private Level currentLevel;

    [Header("Spawn points")]
    [SerializeField] private SpawnPoint[] spawnPoints = new SpawnPoint[0];

    // Internal references
    private int currentWaveIndex = 0;
    private int currentWaveKills = 0;
    private int spawnedCount = 0;
    private int aliveEnemiesCount = 0;
    private float timeFromLastSpawn = 0.0f;

    private WaveInfo currentWave => currentLevel.GetWaves()[currentWaveIndex];

    public event Action OnLevelCompleted;

    private void Start()
    {        
        currentLevel.Init();
    }

    private void Update()
    {
        timeFromLastSpawn = Mathf.Min(timeFromLastSpawn + Time.deltaTime, currentWave.TimeBetweenSpawns);

        if (timeFromLastSpawn >= currentWave.TimeBetweenSpawns && aliveEnemiesCount < currentWave.MaxSpawnedAtOnce
            && spawnedCount < currentWave.CountEntitiesToSpawn)
        {
            SpawnNewEnemy();
        }
    }

    private void SpawnNewEnemy()
    {
        int indexToSpawn = UnityEngine.Random.Range(0, spawnPoints.Length);

        var enemySpawned = spawnPoints[indexToSpawn].SpawnEnemy(currentWave.EntryToSpawn());

        if (enemySpawned == null)
        {            
            return;
        }

        currentWave.ConfirmEntitySpawned();

        timeFromLastSpawn = 0f;
        aliveEnemiesCount++;
        spawnedCount++;

        // This will be implemeted once the enemy behaviour is outlined
        enemySpawned.OnDeath += HandeEnemyDeath;
    }

    public void HandeEnemyDeath()
    {
        currentWaveKills++;
        aliveEnemiesCount--;

        // If we didnt kill all the enemies yet we dont do anything
        if (currentWaveKills < currentWave.CountEntitiesToSpawn)
        {
            return;
        }
        
        if (currentWaveIndex < currentLevel.GetWaves().Length - 1){
            currentWaveIndex++;

            currentWaveKills = 0;
            spawnedCount = 0;
            aliveEnemiesCount = 0;
        }
        else {
            OnLevelCompleted?.Invoke();
        }
    }
}
