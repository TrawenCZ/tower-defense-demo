using System.Linq;
using UnityEngine;

public class RandomTower : Tower
{
    protected override Enemy FindTarget()
    {
        Enemy[] potentialTargets = Physics.OverlapSphere(transform.position, _fireRange)
            .Where(obj => obj.gameObject.TryGetComponent(out Enemy enemy))
            .Select(enemy => enemy.gameObject.GetComponent<Enemy>())
            .ToArray();
        if (potentialTargets.Length > 0) return potentialTargets[Random.Range(0, potentialTargets.Length)];
        return null;
    }

    protected override void FireAtTarget()
    {
        int randomNumber = Random.Range(1, 6);
        switch (randomNumber)
        {
            case int n when (n <= 3):
                LaunchProjectile();
                break;
            case 4:
                LaunchProjectile();
                LaunchProjectile();
                break;
        }
    }
}
