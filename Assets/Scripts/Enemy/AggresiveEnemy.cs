using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggresiveEnemy : Enemy
{
    private const int attackRange = 10;
    private Tower chosenTarget = null;

    // Update is called once per frame
    void Update()
    {
        if (chosenTarget != null) return;
        Tower firstTowerInRange = Physics.OverlapSphere(this.transform.position, attackRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Tower tower))
            .OrderBy(tower => Vector3.Distance(tower.transform.position, this.transform.position))
            .FirstOrDefault()?.gameObject.GetComponent<Tower>();
        if (firstTowerInRange != null)
        {
            this._movementComponent.MoveTowards(firstTowerInRange.transform);
            chosenTarget = firstTowerInRange;
            return;
        }
        this._movementComponent.MoveAlongPath();
    }

    private void OnCollisionEnter(Collision other)
    {
        EnemyCollided(other, Damage);
    }
}
