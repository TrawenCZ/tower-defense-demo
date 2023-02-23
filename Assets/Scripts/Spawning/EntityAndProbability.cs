using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EntityAndProbability
{
    [SerializeField]
    private GameObject entityToSpawn;

    [SerializeField]
    [Range(0, 1)]
    private float probabilityToSpawn;

    [SerializeField]
    private int countEntitiesToSpawn;

    [SerializeField]
    private ParticleSystem VFXWhenSpawned;

    [SerializeField]
    private SpawnTransformModifier particleSystemSpawnModifier;

    [SerializeField]
    private AudioClip SFXWhenSpawn;

    public int CountEntitiesToSpawn { get => countEntitiesToSpawn; }
    public float ProbabilityToSpawn { get => probabilityToSpawn; }
    public GameObject SpawnableEntity { get => entityToSpawn; }
    public ParticleSystem VFXToInstantWhenSpawned { get => VFXWhenSpawned; }
    public SpawnTransformModifier ParticleSpawnTransformModifier { get => particleSystemSpawnModifier; }
    public AudioClip ClipToPlayeWhenSpawned { get => SFXWhenSpawn; }
}