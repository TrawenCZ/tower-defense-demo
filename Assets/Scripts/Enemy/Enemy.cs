using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected MovementComponent _movementComponent;
    [SerializeField] protected HealthComponent _healthComponent;
    [SerializeField] protected ParticleSystem _onDeathParticlePrefab;
    [SerializeField] protected ParticleSystem _onSuccessParticlePrefab;
    [SerializeField] protected LayerMask _attackLayerMask;

    public HealthComponent Health => _healthComponent;
    public event Action OnDeath;
    public int Damage;
    public int Reward;
    public int Speed;
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
        _movementComponent.Init(path, Speed);
    }

    protected void HandleDeath()
    {
        GameObject.FindObjectOfType<Player>().Resources += Reward;
        OnDeath?.Invoke();
        Instantiate(_onDeathParticlePrefab, this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

    private void CastleOrTowerHit()
    {
        Instantiate(_onSuccessParticlePrefab, this.transform.position, this.transform.rotation);
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    protected void EnemyCollided(Collision other, int damageToTower)
    {
        if (other.gameObject.TryGetComponent<Castle>(out Castle hitCastle))
        {
            hitCastle.Health.HealthValue -= Damage;
            CastleOrTowerHit();
        }
        else if (other.gameObject.TryGetComponent<Tower>(out Tower hitTower))
        {
            hitTower.Health.HealthValue -= damageToTower;
            CastleOrTowerHit();
        }
    }
}
