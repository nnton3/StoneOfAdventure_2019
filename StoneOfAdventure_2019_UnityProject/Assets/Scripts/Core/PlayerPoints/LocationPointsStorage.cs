using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using StoneOfAdventure.Core;

public class LocationPointsStorage : MonoBehaviour
{
    private int locationPoints;
    public int LocationPointsValue => locationPoints;
    [SerializeField] private int maxLocationPoints;
    public int LocationPointsMaxValue => maxLocationPoints;
    [HideInInspector] public UnityEvent LocationPointsUpdated;
    [HideInInspector] public UnityEvent LocationCompleted;

    public void AddPoints(int value)
    {
        if (value == 0) return;
        locationPoints += value;
        if (locationPoints > maxLocationPoints) locationPoints = maxLocationPoints;
        if (locationPoints == maxLocationPoints) LocationCompleted?.Invoke();

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
        LocationCompleted.RemoveAllListeners();
    }
}
