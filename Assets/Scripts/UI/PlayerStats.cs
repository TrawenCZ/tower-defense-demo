using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _resourcesText;

    private void Start()
    {
        _player.OnMoneyChanged += ChangeText;
    }

    private void ChangeText(int resources)
    {
        _resourcesText.text = $"Resources: {resources}";
    }
}
