using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class LocationPointsStorage : MonoBehaviour
{
    private int locationPoints;
    public int LocationPointsValue => locationPoints;
    [SerializeField] private int maxLocationPoints;
    public int LocationPointsMaxValue => maxLocationPoints;
    [HideInInspector] public UnityEvent LocationPointsUpdated;
    [Inject] readonly SignalBus signalBus;

    public void AddPoints(int value)
    {
        if (value == 0) return;
        locationPoints += value;
        if (locationPoints > maxLocationPoints) locationPoints = maxLocationPoints;
        if (locationPoints == maxLocationPoints) signalBus.Fire<LocationCompletedSignal>();

        LocationPointsUpdated?.Invoke();
    }

    public void ResetPointValue()
    {
        locationPoints = 0;
        LocationPointsUpdated?.Invoke();
    }

    private void OnDestroy()
    {
        LocationPointsUpdated.RemoveAllListeners();
    }
}
