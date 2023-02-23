using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private HealthComponent _healthComponent;

    private void Start() {
        _healthComponent.OnHealthChange += SetHealthText;
        SetHealthText(_healthComponent.HealthValue);
    }

    private void SetHealthText(int healthNew)
    {
        _healthText.text = $"{healthNew}/{_healthComponent.MaxHealth}";
    }
}
