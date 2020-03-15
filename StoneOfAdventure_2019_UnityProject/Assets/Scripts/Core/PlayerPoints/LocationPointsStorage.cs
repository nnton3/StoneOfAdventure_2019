using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class LocationPointsStorage : MonoBehaviour
{
    private int locationPoints;
    public int LocationPointsValue => locationPoints;
    [SerializeField] private int maxLocationPoints;
    public int LocationPointsMaxValue => maxLocationPoints;
    [Inject] readonly SignalBus signalBus;

    public void AddPoints(int value)
    {
        if (value == 0) return;
        locationPoints += value;
        if (locationPoints > maxLocationPoints) locationPoints = maxLocationPoints;
        if (locationPoints == maxLocationPoints) signalBus.Fire<LocationCompletedSignal>();

        signalBus.Fire(new LocationPointsUpdated { currentValue = locationPoints });
    }

    public void ResetPointValue()
    {
        locationPoints = 0;
        signalBus.Fire(new LocationPointsUpdated { currentValue = locationPoints});
    }
}
