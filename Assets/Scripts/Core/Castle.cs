using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(HealthComponent))]
public class Castle : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;

    public HealthComponent Health => _healthComponent;

    public event Action OnLevelFailed;

    private void Start()
    {
        _healthComponent.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        _healthComponent.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        // Do end of turn stuff
        OnLevelFailed?.Invoke();
    }
}
