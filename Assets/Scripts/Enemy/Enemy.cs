using System;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected MovementComponent _movementComponent;
    [SerializeField] protected HealthComponent _healthComponent;
    [SerializeField] protected ParticleSystem _onDeathParticlePrefab;
    [SerializeField] protected ParticleSystem _onSuccessParticlePrefab;
    [SerializeField] protected LayerMask _attackLayerMask;
    [SerializeField] protected int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private int _speed;

    public HealthComponent Health => _healthComponent;
    public event Action OnDeath;
    protected float TimeSinceLastStop;

    protected void Start()
    {
        _healthComponent.OnDeath += HandleDeath;
        this.TimeSinceLastStop = 0;
        this._movementComponent.MoveAlongPath();
    }

    private void OnDestroy()
    {
        _healthComponent.OnDeath -= HandleDeath;
    }

    public void Init(EnemyPath path)
    {
        _movementComponent.Init(path, _speed);
    }

    protected void HandleDeath()
    {
        GameObject.FindObjectOfType<Player>().Resources += _reward;
        OnDeath?.Invoke();
        Instantiate(_onDeathParticlePrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void CastleOrTowerHit()
    {
        this._reward = 0;
        this.Health.HealthValue = 0;
    }

    protected void EnemyCollided(Collision other, int damageToTower)
    {
        if (other.gameObject.TryGetComponent<Castle>(out Castle hitCastle))
        {
            hitCastle.Health.HealthValue -= _damage;
            CastleOrTowerHit();
        }
        else if (other.gameObject.TryGetComponent<Tower>(out Tower hitTower))
        {
            hitTower.Health.HealthValue -= damageToTower;
            CastleOrTowerHit();
        }
    }
}
