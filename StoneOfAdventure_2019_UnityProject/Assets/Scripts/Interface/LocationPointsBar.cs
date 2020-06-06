using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LocationPointsBar : MonoBehaviour
{
    private Slider slider;
    [Inject] private LocationPointsStorage pointsStorage;
    [Inject] readonly MainLvlConfig config;

    private void Start()
    {
        slider = GetComponent<Slider>();
        SetStartValue();

        pointsStorage.LocationPoints
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(_ => UpdateLocationPointsUI())
            .AddTo(this);
    }

    private void SetStartValue()
    {
        slider.maxValue = config.TargetLocationPointsValue;
        slider.value = pointsStorage.LocationPoints.Value;
    }

    private void UpdateLocationPointsUI()
    {
        slider.value = pointsStorage.LocationPoints.Value;
    }

    internal void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }
}
