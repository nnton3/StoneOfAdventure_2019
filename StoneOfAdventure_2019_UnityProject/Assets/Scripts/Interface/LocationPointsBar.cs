using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LocationPointsBar : MonoBehaviour
{
    private Slider slider;
    [Inject] private SignalBus signalBus;
    [Inject] private LocationPointsStorage pointsStorage;

    private void Start()
    {
        slider = GetComponent<Slider>();
        SetStartValue();

        signalBus.Subscribe<LocationPointsUpdated>(UpdateLocationPointsUI);
    }

    private void SetStartValue()
    {
        slider.maxValue = pointsStorage.LocationPointsMaxValue;
        slider.value = pointsStorage.LocationPointsValue;
    }

    private void UpdateLocationPointsUI(LocationPointsUpdated args)
    {
        slider.value = args.currentValue;
    }

    internal void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }
}
