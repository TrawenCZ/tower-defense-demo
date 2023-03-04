using System.Linq;
using UnityEngine;

public class BasicTower : Tower
{
    protected override Enemy FindTarget()
    {
        return Physics.OverlapSphere(transform.position, _fireRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Enemy enemy))
            .OrderBy(enemy => Vector3.Distance(enemy.transform.position, transform.position))
            .FirstOrDefault()?.gameObject.GetComponent<Enemy>();
    }

    protected override void FireAtTarget()
    {
        LaunchProjectile();
    }
}
