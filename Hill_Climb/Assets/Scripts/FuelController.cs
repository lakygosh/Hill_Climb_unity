using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    [SerializeField] private Image _fuelImage;
    [SerializeField, Range(0.1f, 5f)] private float _fuelDrainSpeed = 1f;
    [SerializeField] private float _maxFuelAmount = 100f;
    [SerializeField] private Gradient _fuelGradient;
    
    private bool called = false;

    private float _currentFuelAmount;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;   
        }
    }

    private void Start()
    {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }

    private void Update()
    {
        _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
        _fuelImage.color = _fuelGradient.Evaluate(_fuelImage.fillAmount);

        if (_currentFuelAmount <= 0f)
        {
            if (!called)
            {
                GameManager.instance.GameOver();
                called = true;
            }
        }
    }

    public void FillFuel()
    {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }
}
