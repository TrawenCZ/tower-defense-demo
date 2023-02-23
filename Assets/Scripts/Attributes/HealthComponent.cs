using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _health = 0;

    public event Action OnDeath;
    public event Action<int> OnHealthChange;

    public int MaxHealth => _maxHealth;

    public int HealthValue
    {
        get { return _health; }
        set
        {
            _health = value;
            OnHealthChange?.Invoke(_health);

            if (_health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }

    public bool IsAlive => _health > 0;

    private void Awake()
    {
        _health = _maxHealth;
    }
}
