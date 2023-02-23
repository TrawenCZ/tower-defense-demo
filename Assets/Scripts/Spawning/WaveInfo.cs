using System;
using UnityEngine;
using Utils;

[Serializable]
public class WaveInfo
{
    [Header("WaveInformation")]
    
    [SerializeField] private int maxSpawnedAtOnce;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private EntityAndProbability[] enemyTypesToSpawn = new EntityAndProbability[0];

    public int MaxSpawnedAtOnce { get => maxSpawnedAtOnce; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public int CountEntitiesToSpawn { get => totalEnemiesInWave; }

    // Internal references

    private WeightedRandomBag<BagEntry> entityBag;
    private int totalEnemiesInWave = -1;

    private WeightedRandomBag<BagEntry>.Entry lastEntry;

    public void InitWave()
    {
        InitializeBag();
        totalEnemiesInWave = GetTotalEntities();
    }

    public int GetTotalEntities()
    {        
        totalEnemiesInWave = 0;
        foreach (var enemyType in enemyTypesToSpawn)
        {
            totalEnemiesInWave += enemyType.CountEntitiesToSpawn;
        }

        return totalEnemiesInWave;
    }


    public BagEntry EntryToSpawn()
    {
        lastEntry = entityBag.GetRandomEntry();

        return lastEntry.Item;
    }

    public void ConfirmEntitySpawned()
    {
        entityBag.RemoveEntry(lastEntry);        
    }

    private void InitializeBag()
    {
        entityBag = new WeightedRandomBag<BagEntry>();

        foreach (var entityAndProb in enemyTypesToSpawn)
        {
            for (int i = 0; i < entityAndProb.CountEntitiesToSpawn; i++)
            {
                entityBag.AddEntry(new BagEntry(entityAndProb.SpawnableEntity, entityAndProb.VFXToInstantWhenSpawned,
                    entityAndProb.ClipToPlayeWhenSpawned, entityAndProb.ParticleSpawnTransformModifier), 
                    entityAndProb.ProbabilityToSpawn);
            }
        }
    }
}


