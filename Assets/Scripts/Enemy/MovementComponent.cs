using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navmeshAgent;

    private EnemyPath _pathToFollow;
    private int _currentPathPoint = 0;
    private Transform _target;

    public bool IsMoving => _navmeshAgent.hasPath;

    public void Init(EnemyPath path, float speed)
    {
        _pathToFollow = path;
        _navmeshAgent.speed = speed;
    }

    private void Update()
    {
        if (!_pathToFollow.Points[_currentPathPoint].IsClose(transform.position) || (_currentPathPoint + 1) == _pathToFollow.Points.Count)
        {
            return;
        }

        _currentPathPoint++;
        if (_target == null)
        {
            MoveAlongPath();
        }
        else
        {
            MoveTowards(_target);
        }
    }

    public void MoveAlongPath()
    {
        _navmeshAgent.SetDestination(_pathToFollow.Points[_currentPathPoint].transform.position);
    }

    public bool MoveTowards(Transform target)
    {
        _target = target;
        return _navmeshAgent.SetDestination(target.position);
    }

    public void CancelMovement()
    {
        _navmeshAgent.ResetPath();
    }
}
