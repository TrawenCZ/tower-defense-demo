using UnityEngine;

[System.Serializable]
public class BagEntry
{
    public BagEntry(GameObject toSpawn, ParticleSystem partToSpawn, AudioClip audToPlay, SpawnTransformModifier transformModifier )
    {
        EntToSpawn = toSpawn;
        VFXWhenSpawned = partToSpawn;
        SFXWhenSpawned = audToPlay;
        ParticleSystemTransformSpawnModifier = transformModifier;
    }

    public GameObject EntToSpawn { get; private set; }
    public ParticleSystem VFXWhenSpawned { get; private set; }
    public AudioClip SFXWhenSpawned { get; private set; }
    public SpawnTransformModifier ParticleSystemTransformSpawnModifier { get; private set; }
}
