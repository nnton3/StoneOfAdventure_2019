using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LocationPointsBar : MonoBehaviour
{
    private LocationPointsStorage pointsStorage;
    private Slider slider;

    private void Start()
    {
        pointsStorage = FindObjectOfType<LocationPointsStorage>();
        slider = GetComponent<Slider>();

        slider.maxValue = pointsStorage.LocationPointsMaxValue;
        UpdateLocationPointsUI();

        pointsStorage.LocationPointsUpdated.AddListener(UpdateLocationPointsUI);
    }

    private void UpdateLocationPointsUI()
    {
        slider.value = pointsStorage.LocationPointsValue;
    }

    internal void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }
}
