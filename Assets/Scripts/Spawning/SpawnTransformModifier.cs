using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnTransformModifier
{
    [SerializeField] private Vector3 positionSpawnOffset = Vector3.zero;
    [SerializeField] private Vector3 scaleMultiplication = Vector3.one;
    [SerializeField] private Vector3 rotationSpawnOffset = Vector3.zero;

    public Vector3 PositionSpawnOffset
    {
        get
        {
            return positionSpawnOffset;
        }
        set
        {
            positionSpawnOffset = value;
        }
    }

    public Vector3 ScaleMultiplication
    {
        get
        {
            return scaleMultiplication;
        }
        set
        {
            scaleMultiplication = value;
        }
    }

    public Vector3 RotationOffset
    {
        get
        {
            return rotationSpawnOffset;
        }
        set
        {
            rotationSpawnOffset = value;
        }
    }
}
