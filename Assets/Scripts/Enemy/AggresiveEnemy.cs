using System.Linq;
using UnityEngine;

public class AggresiveEnemy : Enemy
{
    private const int attackRange = 10;
    private Tower chosenTarget = null;

    void Update()
    {
        if (chosenTarget != null) return;
        Tower firstTowerInRange = Physics.OverlapSphere(transform.position, attackRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Tower tower))
            .OrderBy(tower => Vector3.Distance(tower.transform.position, transform.position))
            .FirstOrDefault()?.gameObject.GetComponent<Tower>();
        if (firstTowerInRange != null)
        {
            _movementComponent.MoveTowards(firstTowerInRange.transform);
            chosenTarget = firstTowerInRange;
            return;
        }
        _movementComponent.MoveAlongPath();
    }

    private void OnCollisionEnter(Collision other)
    {
        EnemyCollided(other, _damage);
    }
}
