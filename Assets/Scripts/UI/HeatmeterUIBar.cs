using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatmeterUIBar : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Heatmeter _heatmeter;
    [SerializeField] private Slider _heatmeterSlider;
    [SerializeField] private Image _heatmeterSliderImage;

    private void Awake()
    {
        if (_player == null) _player = RefLib.Player;
        //if (_heatmeter == null) 
        _heatmeter = _player.GetComponent<Heatmeter>();
    }

    private void OnEnable()
    {
        if (_heatmeter == null) return;

        _heatmeter.OnHeatmeterChanged += UpdateHeatmeterUI;
        _heatmeter.OnShipOverheating += ChangeToOverheatedColor;
        _heatmeter.OnShipCooledDown += ChangeToNormalColor;

    }

    private void OnDisable()
    {
        if (_heatmeter == null) return;

        _heatmeter.OnHeatmeterChanged -= UpdateHeatmeterUI;
        _heatmeter.OnShipOverheating -= ChangeToOverheatedColor;
        _heatmeter.OnShipCooledDown -= ChangeToNormalColor;
    }

    private void UpdateHeatmeterUI(float heatmeterPercentage)
    {
        _heatmeterSlider.value = heatmeterPercentage;
    }

    private void ChangeToOverheatedColor()
    {
        _heatmeterSliderImage.color = Color.red;
    }

    private void ChangeToNormalColor()
    {
        _heatmeterSliderImage.color = Color.white;
    }

}
