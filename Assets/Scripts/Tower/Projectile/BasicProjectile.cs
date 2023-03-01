
public class BasicProjectile : Projectile
{
    protected override void ProjectileLanded(Enemy hitEnemy)
    {
        if (hitEnemy != null)
        {
            hitEnemy.Health.HealthValue -= _damage;
        }
        Destroy(gameObject);
    }
}
