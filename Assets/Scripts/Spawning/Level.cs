using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "HW1/Spawning/Level", order = 0)]
public class Level : ScriptableObject
{  
    [Header("Level wave")]
    [SerializeField] private WaveInfo[] waves;

    public WaveInfo[] GetWaves()
    {
        return waves;
    }

    public void Init()
    {
        foreach (var wave in waves)
        {
            wave.InitWave();
        }
    }
}
