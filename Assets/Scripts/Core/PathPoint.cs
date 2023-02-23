using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [SerializeField] private float _radius = 1.0f;

    public bool IsClose(Vector3 targetPosition)
    {
        return Vector3.Distance(targetPosition, transform.position) < _radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.2f);
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
