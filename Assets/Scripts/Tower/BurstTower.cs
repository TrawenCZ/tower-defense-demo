using System.Linq;
using UnityEngine;

public class BurstTower : Tower
{
    private bool shouldLaunchSecondProjectile = false;

    protected override Enemy FindTarget()
    {
        return Physics.OverlapSphere(transform.position, _fireRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Enemy enemy))
            .OrderByDescending(enemy => enemy.gameObject.GetComponent<Enemy>().Health.HealthValue)
            .FirstOrDefault()?.gameObject.GetComponent<Enemy>();
    }

    protected override void FireAtTarget()
    {
        if (shouldLaunchSecondProjectile)
        {
            LaunchProjectile();
            shouldLaunchSecondProjectile = false;
            _shotDelay = 3.0f; 
        } else
        {
            LaunchProjectile();
            shouldLaunchSecondProjectile = true;
            _shotDelay = 0.2f;
        }
    }
}
