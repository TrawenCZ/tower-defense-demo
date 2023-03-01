using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicTower : Tower
{
    protected override Enemy FindTarget()
    {
        return Physics.OverlapSphere(this.transform.position, _fireRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Enemy enemy))
            .OrderBy(enemy => Vector3.Distance(enemy.transform.position, this.transform.position))
            .FirstOrDefault()?.gameObject.GetComponent<Enemy>();
    }

    protected override void FireAtTarget()
    {
        LaunchProjectile();
    }
}
