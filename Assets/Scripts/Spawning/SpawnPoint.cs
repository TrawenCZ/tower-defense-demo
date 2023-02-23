using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private EnemyPath _enemyPath;
    [SerializeField] private Transform _spawnPlace;

    [Header("Modifiers")]
    [SerializeField] private float _radiusWhereToSpawn = 10f;

    [SerializeField] private SpawnTransformModifier _spawnPointParticleSystemSpawnModifier;
    [SerializeField] private SpawnTransformModifier _spawnPointEnemySpawnModifier;

    public Enemy SpawnEnemy(BagEntry objectToInstantiate)
    {
        NavMeshHit navMeshHit;
        if (!NavMesh.SamplePosition(_spawnPlace.position, out navMeshHit, _radiusWhereToSpawn, 1))
        {
            Debug.Log("No place");
            return null;
        }

        Bounds? objectBounds = null;
        var meshRenderer = objectToInstantiate.EntToSpawn.GetComponentInChildren<Renderer>();
        if (meshRenderer != null)
        {
            objectBounds = meshRenderer.bounds;
        }
        else
        {
            meshRenderer = objectToInstantiate.EntToSpawn.GetComponent<Renderer>();
            if (meshRenderer != null)
            {
                objectBounds = meshRenderer.bounds;
            }
        }

        if (objectBounds.HasValue == false)
        {
            Debug.LogError("Could not get bounds");
        }

        var audioToPlay = objectToInstantiate.SFXWhenSpawned;

        if (audioToPlay != null)
        {
            _audioSource.Stop();
            _audioSource.clip = audioToPlay;
            _audioSource.Play();
        }

        var vfxToSpawn = objectToInstantiate.VFXWhenSpawned;

        if (vfxToSpawn != null)
        {
            SpawnVFX(objectToInstantiate, navMeshHit);
        }

        var newObj = Instantiate(objectToInstantiate.EntToSpawn,
            navMeshHit.position + Vector3.up * objectBounds.Value.size.y / 2 + _spawnPointEnemySpawnModifier.PositionSpawnOffset,
            Quaternion.Euler(_spawnPointEnemySpawnModifier.RotationOffset)).GetComponent<Enemy>();

        if (newObj == null)
        {
            Debug.LogError("Enemy prefabs do not have an Enemy script attached");
            return null;
        }

        newObj.transform.localScale = new Vector3(
            newObj.transform.localScale.x * _spawnPointParticleSystemSpawnModifier.ScaleMultiplication.x,
            newObj.transform.localScale.y * _spawnPointParticleSystemSpawnModifier.ScaleMultiplication.y,
            newObj.transform.localScale.z * _spawnPointParticleSystemSpawnModifier.ScaleMultiplication.z);

        newObj.Init(_enemyPath);
        return newObj;
    }

    private void SpawnVFX(BagEntry objectToInstantiate, NavMeshHit navMeshHit)
    {
        var vfxTransformSpawnModif = objectToInstantiate.ParticleSystemTransformSpawnModifier;

        Debug.Log(vfxTransformSpawnModif.PositionSpawnOffset);

        var vfxSpawn = Instantiate(objectToInstantiate.VFXWhenSpawned,
            navMeshHit.position + vfxTransformSpawnModif.PositionSpawnOffset + _spawnPointParticleSystemSpawnModifier.PositionSpawnOffset,
            Quaternion.Euler(vfxTransformSpawnModif.RotationOffset + _spawnPointParticleSystemSpawnModifier.RotationOffset));

        vfxSpawn.transform.localScale = new Vector3(
            vfxSpawn.transform.localScale.x * vfxTransformSpawnModif.ScaleMultiplication.x * _spawnPointParticleSystemSpawnModifier.ScaleMultiplication.x,
            vfxSpawn.transform.localScale.y * vfxTransformSpawnModif.ScaleMultiplication.y * _spawnPointParticleSystemSpawnModifier.ScaleMultiplication.y,
            vfxSpawn.transform.localScale.z * vfxTransformSpawnModif.ScaleMultiplication.z * _spawnPointParticleSystemSpawnModifier.ScaleMultiplication.z);
    }
}
