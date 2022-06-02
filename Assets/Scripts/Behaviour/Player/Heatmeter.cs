using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatmeter : MonoBehaviour, IReduceHeatlevel
{
    private float _currentHeatlevel;
    public float CurrentHeatlevel
    {
        get { return _currentHeatlevel; }
        set
        {
            if (value > _maximumHeatlevel) value = _maximumHeatlevel;
            if (value < 0) value = 0;

            _currentHeatlevel = value;

            if (value == _maximumHeatlevel)
                OverheatShip();
            else if (_isOverheated && value == 0)
                CoolDownShip();

            OnHeatmeterChanged?.Invoke(HeatmeterPercentage);
        }
    }

    public float HeatmeterPercentage { get { return (CurrentHeatlevel / MaximumHeatlevel) * 100; } }
    
    public delegate void ShipOverheatAction();
    public event ShipOverheatAction OnShipOverheating;

    public delegate void ShipCooledDownAction();
    public event ShipCooledDownAction OnShipCooledDown;

    public delegate void CurrentHeatmeterValueChanged(float heatmeterPercentage);
    public event CurrentHeatmeterValueChanged OnHeatmeterChanged;


    private void CoolDownShip()
    {
        _isOverheated = false;
        Debug.Log("Ship cooled down");
        OnShipCooledDown?.Invoke();
    }


    private void OverheatShip()
    {
        _isOverheated = true;
        Debug.Log("Ship overheated");
        OnShipOverheating?.Invoke();
    }

    [SerializeField] private float _maximumHeatlevel = 200;
    public float MaximumHeatlevel
    {
        get { return _maximumHeatlevel; }
        
    }

    private bool _isOverheated;
    public bool IsOverheated 
    {
        get { return _isOverheated; }
    }

    private readonly float _baseCooldownRate = 0.1f;
    public float CooldownMultiplier = 1;
    [SerializeField] private float _overheatPenalty = 1.8f;

    private float _currentCooldownRate;

    private readonly float _baseCooldownAmount = 1;
    public float CooldownAmountMultipier = 1;

    private void Update()
    {
        CoolDownHeatmeter();
    }

    private void CoolDownHeatmeter()
    {

        if (CurrentHeatlevel <= 0) return;

        if (_currentCooldownRate < 0)
        {
            _currentCooldownRate = CalculateCooldownMultipier();

            CurrentHeatlevel -= _baseCooldownAmount * CooldownAmountMultipier;

        }
        else _currentCooldownRate -= Time.deltaTime;


    }

    private float CalculateCooldownMultipier()
    {
        float newCooldownRate = _baseCooldownRate;

        if (_isOverheated) newCooldownRate *= _overheatPenalty;

        return newCooldownRate;
    }


    public void AddHeatlevel(float heatleavelToAdd)
    {
        if (heatleavelToAdd <= 0) return;
        if (_isOverheated) return;

        CurrentHeatlevel += heatleavelToAdd; 
    }

    public void ReduceHeatlevel(float amountToReduce) 
    {
        if(amountToReduce <= 0) return;
        CurrentHeatlevel -= amountToReduce;
    }
}
