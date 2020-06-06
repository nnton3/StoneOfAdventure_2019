using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class LocationPointsStorage : MonoBehaviour
{
    [Inject] readonly MainLvlConfig config;
    [Inject] readonly SignalBus signalBus;

    public ReactiveProperty<int> LocationPoints { get; private set; }

    private void Awake()
    {
        LocationPoints = new ReactiveProperty<int>(0);
    }

    public void AddPoints(int value)
    {
        if (value == 0) return;
        LocationPoints.Value += value;
        if (LocationPoints.Value > config.TargetLocationPointsValue) LocationPoints.Value = config.TargetLocationPointsValue;
        if (LocationPoints.Value == config.TargetLocationPointsValue) signalBus.Fire<LocationMissionComplete>();
    }

    public void ResetPointValue()
    {
        LocationPoints.Value = 0;
    }
}
