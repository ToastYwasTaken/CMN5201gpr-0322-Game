using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private EntityStats _entityStats;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _armorSlider;

    private void Awake()
    {
        _entityStats = _player.GetComponent<EntityStats>();
    }

    private void OnEnable()
    {
        if (_entityStats == null) return;

        _entityStats.OnHealthPercentageChanged += UpdateHealthUI;
        _entityStats.OnArmorPercentageChanged += UpdateArmorUI;
    }

    private void OnDisable()
    {
        if (_entityStats == null) return;
    }

    private void UpdateHealthUI(float newPercentage)
    {
        _healthSlider.value = newPercentage;
    }

    private void UpdateArmorUI(float newPercentage)
    {
        _armorSlider.value = newPercentage;
    }
}
