using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(HealthComponent))]
public class Tower : MonoBehaviour
{
    [SerializeField] protected LayerMask _enemyLayerMask;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] protected Projectile _projectilePrefab;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] protected Transform _objectToPan;
    [SerializeField] protected Transform _projectileSpawn;
    [SerializeField] private GameObject _previewPrefab;

    public HealthComponent Health => _healthComponent;
    public BoxCollider Collider => _boxCollider;
    public GameObject BuildingPreview => _previewPrefab;

    public int Price;
    public int FireRange;
    public float ShotDelay;
    private float shotCooldown;
    protected Enemy ChosenTarget;

    protected void Start()
    {
        _healthComponent.OnDeath += HandleDeath;
        this.shotCooldown = 0;
        this.ChosenTarget = null;
    }

    private void OnDestroy()
    {
        _healthComponent.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }

    protected bool TargetInRange()
    {
        return Vector3.Distance(this.transform.position, ChosenTarget.transform.position) <= FireRange;
    }

    protected virtual void FireAtTarget()
    {
        throw new NotImplementedException();
    }

    protected void LaunchProjectile()
    {
        Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawn.position, _projectileSpawn.rotation);
        projectile.Target = ChosenTarget;
    }

    protected virtual Enemy FindTarget()
    {
        throw new NotImplementedException();
    }

    protected bool ShouldShoot()
    {
        this.shotCooldown -= Time.deltaTime;
        if (shotCooldown <= 0)
        {
            return true;
        }
        return false;
    }

    private void LookAtTarget()
    {
        this._objectToPan.LookAt(ChosenTarget.transform.position);
    }

    protected void Update()
    {
        if (ChosenTarget != null) LookAtTarget();
        if (!ShouldShoot()) return;
        if (ChosenTarget != null && TargetInRange())
        {
            FireAtTarget();
            this.shotCooldown = ShotDelay;
            return;
        }
        ChosenTarget = FindTarget();
        if (ChosenTarget != null)
        {
            LookAtTarget();
            FireAtTarget();
            this.shotCooldown = ShotDelay;
        }
    }
}