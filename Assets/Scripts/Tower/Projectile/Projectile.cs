using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected LayerMask _enemyLayerMask;
    [SerializeField] protected ParticleSystem _onHitParticleSystem;

    public int Damage;
    public int Speed;
    public float LifeSpan;
    private float timeSinceLaunch;
    public Enemy Target;
    private Vector3 lastTargetLocation;
    private Vector3 directionOfShot;
    private bool directionSet;

    protected virtual void ProjectileLanded(Enemy hitEnemy)
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        this.timeSinceLaunch += Time.deltaTime;
        if (this.timeSinceLaunch > LifeSpan) DestroyProjectile(null);
        if (Target == null)
        {
            if (!directionSet)
            {
                directionSet = true;
                directionOfShot = Vector3.Normalize(lastTargetLocation - this.transform.position);
            }
            this.transform.position += Speed * Time.deltaTime * directionOfShot;
            
        }
        else
        {
            lastTargetLocation = Target.transform.position + new Vector3(0, 0.75f, 0);
            this.transform.position += Speed * Time.deltaTime * Vector3.Normalize(lastTargetLocation - this.transform.position);
        }
    }

    private void DestroyProjectile(Enemy hitEnemy)
    {
        Instantiate(_onHitParticleSystem, this.transform.position, Quaternion.identity);
        ProjectileLanded(hitEnemy);
    }

    protected void OnTriggerEnter(Collider other)
    {
        DestroyProjectile(other.gameObject.GetComponent<Enemy>());
    }
}
