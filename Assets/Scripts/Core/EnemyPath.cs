using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private List<PathPoint> _pathPoints = new List<PathPoint>();

    public IReadOnlyList<PathPoint> Points => _pathPoints;
}
