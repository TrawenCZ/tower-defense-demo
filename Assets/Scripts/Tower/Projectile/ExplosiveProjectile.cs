using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExplosiveProjectile : Projectile
{
    private const int explosiveRange = 5;

    private void DoDamage()
    {
         Physics.OverlapSphere(transform.position, explosiveRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Enemy enemy))
            .Select(enemy => enemy.gameObject.GetComponent<Enemy>())
            .ToList()
            .ForEach(enemy => enemy.Health.HealthValue -= _damage);
    }

    protected override void ProjectileLanded(Enemy hitEnemy)
    {
        DoDamage();
        Destroy(gameObject);
    }
}
